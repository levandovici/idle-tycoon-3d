using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    private TerrainData _terrainData;

    [SerializeField]
    private Dictionary<Vector2Int, GameObject> _terrain = new Dictionary<Vector2Int, GameObject>();

    [SerializeField]
    private GameObject _cell;

    [SerializeField]
    private GameObject _cellAdd;

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



    private void Generate()
    {
        CellData[] cells = _terrainData.Cells;

        for (int i = 0; i < cells.Length; i++)
        {
            Vector3 pos = new Vector3(cells[i].Position.x / 2 * _cellSize + cells[i].Position.x / 2 * _connectionSize,
                0f, cells[i].Position.y / 2 * _cellSize + cells[i].Position.y / 2 * _connectionSize);

            GameObject obj = Instantiate(_cell, pos, Quaternion.identity);

            _terrain.Add(cells[i].Position, obj);
        }


        ConnectionData[] connections = _terrainData.Connections;

        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].Type == ConnectionType.None)
                continue;

            Vector3 offest = Vector3.zero;

            Quaternion rotation = Quaternion.identity;

            if (connections[i].Position.x % 2 != 0)
            {
                offest.x = (connections[i].Position.x < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;
            }

            if (connections[i].Position.y % 2 != 0)
            {
                offest.z = (connections[i].Position.y < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;

                rotation = Quaternion.Euler(0f, 90f, 0f);
            }

            Vector3 pos = new Vector3(connections[i].Position.x / 2 * _connectionSize + connections[i].Position.x / 2 * _cellSize,
                0f, connections[i].Position.y / 2 * _connectionSize + connections[i].Position.y / 2 * _cellSize);

            GameObject obj = null;

            if (connections[i].Type == ConnectionType.Road)
            {
                obj = Instantiate(_roadConnection, pos + offest, rotation);
            }
            else if (connections[i].Type == ConnectionType.Grass)
            {
                obj = Instantiate(_grassConnection, pos + offest, rotation);
            }

            _terrain.Add(connections[i].Position, obj);
        }


        JoinData[] joints = _terrainData.Joints;

        for(int i = 0; i < joints.Length; i++)
        {
            Vector3 offest = Vector3.zero;

            Quaternion rotation = Quaternion.identity;

            if (joints[i].Position.x % 2 != 0)
            {
                offest.x = (joints[i].Position.x < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;
            }

            if (joints[i].Position.y % 2 != 0)
            {
                offest.z = (joints[i].Position.y < 0 ? -_cellSize - _connectionSize : _cellSize + _connectionSize) * 0.5f;
            }

            Vector3 pos = new Vector3(joints[i].Position.x / 2 * _connectionSize + joints[i].Position.x / 2 * _cellSize,
                0f, joints[i].Position.y / 2 * _connectionSize + joints[i].Position.y / 2 * _cellSize);

            GameObject obj = null;

            switch (joints[i].Type)
            {
                case JoinType.RoadX:
                    obj = Instantiate(_roadXJoint, pos + offest, rotation);
                    break;

                case JoinType.RoadT:
                    if (!joints[i].Left)
                    {
                        rotation = Quaternion.Euler(0f, 90f, 0f);
                    }
                    else if (!joints[i].Right)
                    {
                        rotation = Quaternion.Euler(0f, -90f, 0f);
                    }
                    else if(!joints[i].Top)
                    {
                        rotation = Quaternion.Euler(0f, 180f, 0f);
                    }

                    obj = Instantiate(_roadTJoint, pos + offest, rotation);
                    break;

                case JoinType.RoadI:
                    if (joints[i].Left)
                    {
                        rotation = Quaternion.Euler(0f, 90f, 0f);
                    }

                    obj = Instantiate(_roadIJoint, pos + offest, rotation);
                    break;

                case JoinType.RoadL:
                    if (joints[i].Left)
                    {
                        if (joints[i].Bottom)
                        {
                            rotation = Quaternion.Euler(0f, -90f, 0f);
                        }
                    }
                    else
                    {
                        if (joints[i].Top)
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
                    if (joints[i].Left)
                    {
                        rotation = Quaternion.Euler(0f, -90f, 0f);
                    }
                    else if (joints[i].Right)
                    {
                        rotation = Quaternion.Euler(0f, 90f, 0f);
                    }
                    else if (joints[i].Bottom)
                    {
                        rotation = Quaternion.Euler(0f, 180f, 0f);
                    }

                    obj = Instantiate(_roadUJoint, pos + offest, rotation);
                    break;

                case JoinType.Grass:
                    obj = Instantiate(_grassJoint, pos + offest, rotation);
                    break;

                default:
                    continue;
            }

            _terrain.Add(joints[i].Position, obj);
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


        IEnumerable<Vector2Int> list = leftCells.Concat(rightCells).Concat(bottomCells).Concat(topCells)

        foreach (Vector2Int position in list)
        {
            Vector3 pos = new Vector3(position.x / 2 * _cellSize + position.x / 2 * _connectionSize,
                0f, position.y / 2 * _cellSize + position.y / 2 * _connectionSize);

            GameObject obj = Instantiate(_cellAdd, pos, Quaternion.identity);

            _terrain.Add(position, obj);
        }
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
