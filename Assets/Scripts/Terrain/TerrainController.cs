using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    private TerrainData _terrainData;

    [SerializeField]
    private Dictionary<Vector2Int, GameObject> _terrain = new Dictionary<Vector2Int, GameObject>();

    [SerializeField]
    private Cell _cell;

    [SerializeField]
    private CellAdd _cellAdd;

    [SerializeField]
    private GameObject _roadConnection;

    [SerializeField]
    private GameObject _grassConnection;

    [SerializeField]
    private GameObject _roadXJoint;

    [SerializeField]
    private GameObject _roadTJoint;

    [SerializeField]
    private GameObject _roadIJoint;

    [SerializeField]
    private GameObject _roadLJoint;

    [SerializeField]
    private GameObject _roadUJoint;

    [SerializeField]
    private GameObject _grassJoint;

    [SerializeField]
    private float _cellSize = 40f;

    [SerializeField]
    private float _connectionSize = 15f;



    public void Setup(TerrainData terrain)
    {
        _terrainData = terrain;

        Generate();

        GenerateSale();
    }



    public void Bounds(out float minX, out float maxX, out float minZ, out float maxZ)
    {
        minX = _terrainData.MinX / 2 * _cellSize + _terrainData.MinX / 2 * _connectionSize;

        maxX = _terrainData.MaxX / 2 * _cellSize + _terrainData.MaxX / 2 * _connectionSize;

        minZ = _terrainData.MinZ / 2 * _cellSize + _terrainData.MinZ / 2 * _connectionSize;

        maxZ = _terrainData.MaxZ / 2 * _cellSize + _terrainData.MaxZ / 2 * _connectionSize;
    }



    public void AddNewCell(Vector2Int position)
    {
        _terrainData.AddNewCell(position);


        for (int x = position.x - 1; x <= position.x + 1; x++)
        {
            for (int z = position.y - 1; z <= position.y + 1; z++)
            {
                if (_terrain.TryGetValue(new Vector2Int(x, z), out GameObject obj))
                {
                    Destroy(obj);

                    _terrain.Remove(new Vector2Int(x, z));
                }
            }
        }


        if (_terrainData.ContainsCell(position, out CellData cell))
        {
            GenerateCell(cell);
        }


        if (_terrainData.ContainsRoad(position + new Vector2Int(-1, 0), out ConnectionData left))
        {
            GenerateRoad(left.Position, left.Type);
        }

        if (_terrainData.ContainsRoad(position + new Vector2Int(1, 0), out ConnectionData right))
        {
            GenerateRoad(right.Position, right.Type);
        }

        if (_terrainData.ContainsRoad(position + new Vector2Int(0, -1), out ConnectionData bottom))
        {
            GenerateRoad(bottom.Position, bottom.Type);
        }

        if (_terrainData.ContainsRoad(position + new Vector2Int(0, 1), out ConnectionData top))
        {
            GenerateRoad(top.Position, top.Type);
        }


        if (_terrainData.ContainsJoint(position + new Vector2Int(-1, -1), out JoinData leftBottom))
        {
            GenerateJoint(leftBottom.Position, leftBottom.Type,
                leftBottom.Left, leftBottom.Right, leftBottom.Bottom, leftBottom.Top);
        }

        if (_terrainData.ContainsJoint(position + new Vector2Int(-1, 1), out JoinData leftTop))
        {
            GenerateJoint(leftTop.Position, leftTop.Type,
                leftTop.Left, leftTop.Right, leftTop.Bottom, leftTop.Top);
        }

        if (_terrainData.ContainsJoint(position + new Vector2Int(1, -1), out JoinData rightBottom))
        {
            GenerateJoint(rightBottom.Position, rightBottom.Type,
                rightBottom.Left, rightBottom.Right, rightBottom.Bottom, rightBottom.Top);
        }

        if (_terrainData.ContainsJoint(position + new Vector2Int(1, 1), out JoinData rightTop))
        {
            GenerateJoint(rightTop.Position, rightTop.Type,
                rightTop.Left, rightTop.Right, rightTop.Bottom, rightTop.Top);
        }


        GenerateSale();
    }

    public void BuildOnCell(Vector2Int position, EBuilding building)
    {
        _terrainData.BuildOnCell(position, building);

        UpdateCell(position);
    }

    public void UpgradeCell(Vector2Int position)
    {
        _terrainData.UpgradeCell(position);

        UpdateCell(position);
    }



    private void UpdateCell(Vector2Int position)
    {
        if (_terrain.TryGetValue(position, out GameObject obj))
        {
            Destroy(obj);

            _terrain.Remove(position);
        }

        if (_terrainData.ContainsCell(position, out CellData cell))
        {
            GenerateCell(cell);
        }
    }



    private void Generate()
    {
        CellData[] cells = _terrainData.Cells;

        for (int i = 0; i < cells.Length; i++)
        {
            GenerateCell(cells[i]);
        }


        ConnectionData[] connections = _terrainData.Connections;

        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].Type == ConnectionType.None)
                continue;

            GenerateRoad(connections[i].Position, connections[i].Type);
        }


        JoinData[] joints = _terrainData.Joints;

        for(int i = 0; i < joints.Length; i++)
        {
            GenerateJoint(joints[i].Position, joints[i].Type,
                joints[i].Left, joints[i].Right, joints[i].Bottom, joints[i].Top);
        }
    }

    private void GenerateSale()
    {
        CellData[] cells = _terrainData.Cells;


        List<Vector2Int> leftCells = new List<Vector2Int>();

        List<Vector2Int> rightCells = new List<Vector2Int>();

        List<Vector2Int> topCells = new List<Vector2Int>();

        List<Vector2Int> bottomCells = new List<Vector2Int>();


        int minX = _terrainData.MinX;

        int maxX = _terrainData.MaxX;

        int minZ = _terrainData.MinZ;

        int maxZ = _terrainData.MaxZ;



        void ProcessBottom(int z)
        {
            for (int x = minX; x <= maxX; x += 2)
            {
                if (!_terrainData.ContainsCell(x, z, out CellData cell))
                {
                    bottomCells.Add(new Vector2Int(x, z));
                }
            }
        }

        void ProcessTop(int z)
        {
            for (int x = minX; x <= maxX; x += 2)
            {
                if (!_terrainData.ContainsCell(x, z, out CellData cell))
                {
                    topCells.Add(new Vector2Int(x, z));
                }
            }
        }

        void ProcessLeft(int x)
        {
            for (int z = minZ; z <= maxZ; z += 2)
            {
                if (!_terrainData.ContainsCell(x, z, out CellData cell))
                {
                    leftCells.Add(new Vector2Int(x, z));
                }
            }
        }

        void ProcessRight(int x)
        {
            for (int z = minZ; z <= maxZ; z += 2)
            {
                if (!_terrainData.ContainsCell(x, z, out CellData cell))
                {
                    rightCells.Add(new Vector2Int(x, z));
                }
            }
        }


        void ProcessHorizontal()
        {
            ProcessBottom(minZ);

            ProcessTop(maxZ);


            if (topCells.Count > 0)
            {
                maxZ -= 2;
            }
            else
            {
                ProcessTop(maxZ + 2);
            }

            if (bottomCells.Count > 0)
            {
                minZ += 2;
            }
            else
            {
                ProcessBottom(minZ - 2);
            }
        }

        void ProcessVertical()
        {
            ProcessLeft(minX);

            ProcessRight(maxX);


            if (leftCells.Count > 0)
            {
                minX += 2;
            }
            else
            {
                ProcessLeft(minX - 2);
            }

            if (rightCells.Count > 0)
            {
                maxX -= 2;
            }
            else
            {
                ProcessRight(maxX + 2);
            }
        }



        if (_terrainData.SizeX > _terrainData.SizeZ)
        {
            ProcessVertical();

            ProcessHorizontal();
        }
        else
        {
            ProcessHorizontal();

            ProcessVertical();
        }


        IEnumerable<Vector2Int> list = leftCells.Concat(rightCells).Concat(bottomCells).Concat(topCells);

        foreach (Vector2Int position in list)
        {
            if (_terrain.ContainsKey(position))
                continue;

            Vector3 pos = new Vector3(position.x / 2 * _cellSize + position.x / 2 * _connectionSize,
                0f, position.y / 2 * _cellSize + position.y / 2 * _connectionSize);

            CellAdd obj = Instantiate(_cellAdd, pos, Quaternion.identity);

            obj.Setup(position);

            _terrain.Add(position, obj.gameObject);
        }
    }



    private void GenerateCell(CellData cellData)
    {
        Vector3 pos = new Vector3(cellData.Position.x / 2 * _cellSize + cellData.Position.x / 2 * _connectionSize,
                0f, cellData.Position.y / 2 * _cellSize + cellData.Position.y / 2 * _connectionSize);

        Cell cell = Instantiate(_cell, pos, Quaternion.identity);

        cell.Setup(cellData);

        _terrain.Add(cellData.Position, cell.gameObject);

        if (cellData.Building != EBuilding.None)
        {
            GenerateBuilding(pos, cellData.Building, cellData.Level, cell.transform);
        }
    }

    private void GenerateBuilding(Vector3 position, EBuilding building, int level, Transform cell)
    {
        string path;

        Price production;

        float productionDelay;

        switch (building)
        {
            case EBuilding.Factory:
                path = $"Buildings/Factory/Factory-Level-{level}";

                production = Balance.Factory.Production(level);

                productionDelay = Balance.Factory.ProductionDelay(level);
                break;

            case EBuilding.House:
                path = $"Buildings/House/House-Level-{level}";

                production = Balance.House.Production(level);

                productionDelay = Balance.House.ProductionDelay(level);
                break;

            case EBuilding.Production:
                path = $"Buildings/Production/Production-Level-{level}";

                production = Balance.Production.Production(level);

                productionDelay = Balance.Production.ProductionDelay(level);
                break;

            case EBuilding.Warehouse:
                path = $"Buildings/Warehouse/Warehouse-Level-{level}";

                production = Balance.Warehouse.Production(level);

                productionDelay = Balance.Warehouse.ProductionDelay(level);
                break;

            default:
                throw new NotImplementedException();
        }

        Building prefab = Resources.Load<Building>(path);

        Building build = Instantiate(prefab, position, Quaternion.identity, cell);

        build.Setup(production, productionDelay);

        Resources.UnloadUnusedAssets();
    }

    private void GenerateRoad(Vector2Int position, ConnectionType connectionType)
    {
        Vector3 offest = Vector3.zero;

        Quaternion rotation = Quaternion.identity;

        if (position.x % 2 != 0)
        {
            offest.x = (position.x < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;
        }

        if (position.y % 2 != 0)
        {
            offest.z = (position.y < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;

            rotation = Quaternion.Euler(0f, 90f, 0f);
        }

        Vector3 pos = new Vector3(position.x / 2 * _connectionSize + position.x / 2 * _cellSize,
            0f, position.y / 2 * _connectionSize + position.y / 2 * _cellSize);

        GameObject obj = null;

        if (connectionType == ConnectionType.Road)
        {
            obj = Instantiate(_roadConnection, pos + offest, rotation);
        }
        else if (connectionType == ConnectionType.Grass)
        {
            obj = Instantiate(_grassConnection, pos + offest, rotation);
        }

        _terrain.Add(position, obj);
    }

    private void GenerateJoint(Vector2Int position, JoinType joinType, bool left, bool right, bool bottom, bool top)
    {
        Vector3 offest = Vector3.zero;

        Quaternion rotation = Quaternion.identity;

        if (position.x % 2 != 0)
        {
            offest.x = (position.x < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;
        }

        if (position.y % 2 != 0)
        {
            offest.z = (position.y < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;
        }

        Vector3 pos = new Vector3(position.x / 2 * _connectionSize + position.x / 2 * _cellSize,
            0f, position.y / 2 * _connectionSize + position.y / 2 * _cellSize);

        GameObject obj = null;

        switch (joinType)
        {
            case JoinType.RoadX:
                obj = Instantiate(_roadXJoint, pos + offest, rotation);
                break;

            case JoinType.RoadT:
                if (!left)
                {
                    rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else if (!right)
                {
                    rotation = Quaternion.Euler(0f, -90f, 0f);
                }
                else if (!top)
                {
                    rotation = Quaternion.Euler(0f, 180f, 0f);
                }

                obj = Instantiate(_roadTJoint, pos + offest, rotation);
                break;

            case JoinType.RoadI:
                if (left)
                {
                    rotation = Quaternion.Euler(0f, 90f, 0f);
                }

                obj = Instantiate(_roadIJoint, pos + offest, rotation);
                break;

            case JoinType.RoadL:
                if (left)
                {
                    if (bottom)
                    {
                        rotation = Quaternion.Euler(0f, -90f, 0f);
                    }
                }
                else
                {
                    if (top)
                    {
                        rotation = Quaternion.Euler(0f, 90f, 0f);
                    }
                    else
                    {
                        rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                }

                obj = Instantiate(_roadLJoint, pos + offest, rotation);
                break;

            case JoinType.RoadU:
                if (left)
                {
                    rotation = Quaternion.Euler(0f, -90f, 0f);
                }
                else if (right)
                {
                    rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else if (bottom)
                {
                    rotation = Quaternion.Euler(0f, 180f, 0f);
                }

                obj = Instantiate(_roadUJoint, pos + offest, rotation);
                break;

            case JoinType.Grass:
                obj = Instantiate(_grassJoint, pos + offest, rotation);
                break;

            default:
                return;
        }

        _terrain.Add(position, obj);
    }




    [ContextMenu("Generate Random")]
    private void GenerateRandom()
    {
        foreach (GameObject o in _terrain.Values)
        {
            Destroy(o);
        }

        _terrain = new Dictionary<Vector2Int, GameObject>();

        _terrainData = new TerrainData(3, 3);

        Generate();
    }
}
