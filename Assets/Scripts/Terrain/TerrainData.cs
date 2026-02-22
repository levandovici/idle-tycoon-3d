using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Linq;
using System.Xml.Schema;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

[Serializable]
public class TerrainData
{
    [SerializeField]
    private int _sizeX = 3;

    [SerializeField]
    private int _sizeZ = 3;

    [SerializeField]
    private int _minX = -2;

    [SerializeField]
    private int _maxX = 2;

    [SerializeField]
    private int _minZ = -2;

    [SerializeField]
    private int _maxZ = 2;

    [SerializeField]
    private int _maxSizeX = 32;

    [SerializeField]
    private int _maxSizeZ = 32;



    private Dictionary<Vector2Int, CellData> _cells = new Dictionary<Vector2Int, CellData>();

    private Dictionary<Vector2Int, ConnectionData> _connections = new Dictionary<Vector2Int, ConnectionData>();

    private Dictionary<Vector2Int, JoinData> _joints = new Dictionary<Vector2Int, JoinData>();



    public int SizeX
    {
        get
        {
            return _sizeX;
        }

        private set
        {
            _sizeX = value;
        }
    }

    public int SizeZ
    {
        get
        {
            return _sizeZ;
        }

        private set
        {
            _sizeZ = value;
        }
    }

    public int MinX
    {
        get
        {
            return _minX;
        }

        private set
        {
            _minX = value;
        }
    }

    public int MaxX
    {
        get
        {
            return _maxX;
        }

        private set
        {
            _maxX = value;
        }
    }

    public int MinZ
    {
        get
        {
            return _minZ;
        }

        private set
        {
            _minZ = value;
        }
    }

    public int MaxZ
    {
        get
        {
            return _maxZ;
        }

        private set
        {
            _maxZ = value;
        }
    }

    public int MaxSizeX
    {
        get
        {
            return _maxSizeX;
        }

        private set
        {
            _maxSizeX = value;
        }
    }

    public int MaxSizeZ
    {
        get
        {
            return _maxSizeZ;
        }

        private set
        {
            _maxSizeZ = value;
        }
    }


    public CellData[] Cells
    {
        get
        {
            return _cells.Values.ToArray();
        }
    }

    public ConnectionData[] Connections
    {
        get
        {
            return _connections.Values.ToArray();
        }
    }

    public JoinData[] Joints
    {
        get
        {
            return _joints.Values.ToArray();
        }
    }



    public TerrainData(int sizeX, int sizeZ) : this(sizeX, sizeZ, 32, 32)
    {

    }

    public TerrainData(int sizeX, int sizeZ, int maxSizeX, int maxSizeZ)
    {
        SizeX = sizeX;

        SizeZ = sizeZ;


        MinX = -sizeX / 2 * 2;

        MaxX = (sizeX - 1) / 2 * 2;

        MinZ = -sizeZ / 2 * 2;

        MaxZ = (sizeZ - 1) / 2 * 2;


        MaxSizeX = maxSizeX;

        MaxSizeZ = maxSizeZ;


        Generate();
    }



    public bool ContainsCell(int positionX, int positionZ, out CellData cell)
    {
        return ContainsCell(new Vector2Int(positionX, positionZ), out cell);
    }

    public bool ContainsCell(Vector2Int position, out CellData cell)
    {
        bool exists = _cells.ContainsKey(position);

        if (!exists)
        {
            cell = null;

            return false;
        }

        return _cells.TryGetValue(position, out cell);
    }



    private void Generate()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int z = 0; z < _sizeZ; z++)
            {
                int cellX = (x - _sizeX / 2) * 2;

                int cellZ = (z - _sizeZ / 2) * 2;

                AddCell(cellX, cellZ);

                Debug.Log($"Cell: {cellX} : {cellZ}");
            }
        }

        for (int x = -_sizeX / 2 * 2; x <= (_sizeX - 1) / 2 * 2; x += 2)
        {
            for (int z = -_sizeZ / 2 * 2; z <= (_sizeZ - 1) / 2 * 2; z += 2)
            {
                Debug.Log($"{x} : {z}");

                bool previousX = _connections.TryGetValue(new Vector2Int(x - 1, z), out ConnectionData left);

                bool previousZ = _connections.TryGetValue(new Vector2Int(x, z - 1), out ConnectionData bottom);


                bool leftRoad = UnityEngine.Random.Range(0, 2) == 0;

                bool bottomRoad = UnityEngine.Random.Range(0, 2) == 0;


                if (!leftRoad && !bottomRoad)
                {
                    if (UnityEngine.Random.Range(0, 2) == 0)
                    {
                        leftRoad = true;
                    }
                    else
                    {
                        bottomRoad = true;
                    }
                }


                bool rightRoad = UnityEngine.Random.Range(0, 2) == 0;

                bool topRoad = UnityEngine.Random.Range(0, 2) == 0;


                if (leftRoad)
                {
                    if (!previousX)
                    {
                        _connections.TryAdd(new Vector2Int(x - 1, z), new ConnectionData(x - 1, z, ConnectionType.Road));
                    }
                    else
                    {
                        left.Type = ConnectionType.Road;
                    }
                }
                else if (!previousX)
                {
                    _connections.TryAdd(new Vector2Int(x - 1, z), new ConnectionData(x - 1, z, ConnectionType.Grass));
                }

                if (bottomRoad)
                {
                    if (!previousZ)
                    {
                        _connections.TryAdd(new Vector2Int(x, z - 1), new ConnectionData(x, z - 1, ConnectionType.Road));
                    }
                    else
                    {
                        bottom.Type = ConnectionType.Road;
                    }
                }
                else if (!previousZ)
                {
                    _connections.TryAdd(new Vector2Int(x, z - 1), new ConnectionData(x, z - 1, ConnectionType.Grass));
                }

                if (rightRoad)
                {
                    _connections.TryAdd(new Vector2Int(x + 1, z), new ConnectionData(x + 1, z, ConnectionType.Road));
                }
                else
                {
                    _connections.TryAdd(new Vector2Int(x + 1, z), new ConnectionData(x + 1, z, ConnectionType.Grass));
                }

                if (topRoad)
                {
                    _connections.TryAdd(new Vector2Int(x, z + 1), new ConnectionData(x, z + 1, ConnectionType.Road));
                }
                else
                {
                    _connections.TryAdd(new Vector2Int(x, z + 1), new ConnectionData(x, z + 1, ConnectionType.Grass));
                }
            }
        }

        for (int x = -_sizeX / 2 * 2 - 1; x <= (_sizeX - 1) / 2 * 2 + 1; x += 2)
        {
            for (int z = -_sizeZ / 2 * 2 - 1; z <= (_sizeZ - 1) / 2 * 2 + 1; z += 2)
            {
                bool left = _connections.TryGetValue(new Vector2Int(x - 1, z), out ConnectionData leftConnection);

                bool right = _connections.TryGetValue(new Vector2Int(x + 1, z), out ConnectionData rightConnection);

                bool top = _connections.TryGetValue(new Vector2Int(x, z + 1), out ConnectionData topConnection);

                bool bottom = _connections.TryGetValue(new Vector2Int(x, z - 1), out ConnectionData bottomConnection);

                Vector2Int pos = new Vector2Int(x, z);

                _joints.Add(pos, new JoinData(pos,
                    left && leftConnection.Type == ConnectionType.Road,
                    right && rightConnection.Type == ConnectionType.Road,
                    top && topConnection.Type == ConnectionType.Road,
                    bottom && bottomConnection.Type == ConnectionType.Road));
            }
        }
    }

    private void AddCell(int positionX, int positionZ, bool resize = false)
    {
        _cells.Add(new Vector2Int(positionX, positionZ), new CellData(positionX, positionZ));

        if (resize)
        {
            Resize();
        } 
    }

    private void Resize()
    {
        int xMin = int.MaxValue;

        int xMax = int.MinValue;

        int zMin = int.MaxValue;

        int zMax = int.MinValue;

        foreach (CellData cell in _cells.Values)
        {
            if (cell.Position.x < xMin)
            {
                xMin = cell.Position.x;
            }
            else if (cell.Position.x > xMax)
            {
                xMax = cell.Position.x;
            }

            if (cell.Position.y < zMin)
            {
                zMin = cell.Position.y;
            }
            else if (cell.Position.y > zMax)
            {
                zMax = cell.Position.y;
            }
        }

        SizeX = (Mathf.Abs(xMin) + xMax) / 2 + 1;

        SizeZ = (Mathf.Abs(zMin) + zMax) / 2 + 1;

        MinX = xMin;

        MaxX = xMax;

        MinZ = zMin;

        MaxZ = zMax;
    }
}
