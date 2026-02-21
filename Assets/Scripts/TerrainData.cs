using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Linq;

[Serializable]
public class TerrainData
{
    [SerializeField]
    private int _sizeX = 3;

    [SerializeField]
    private int _sizeZ = 3;

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
    }

    public int SizeZ
    {
        get
        {
            return _sizeZ;
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
        _sizeX = sizeX;

        _sizeZ = sizeZ;

        _maxSizeX = maxSizeX;

        _maxSizeZ = maxSizeZ;

        Generate();
    }



    private void Generate()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int z = 0; z < _sizeZ; z++)
            {
                int cellX = (x - _sizeX / 2) * 2;

                int cellZ = (z - _sizeZ / 2) * 2;

                _cells.Add(new Vector2Int(cellX, cellZ), new CellData(cellX, cellZ));

                Debug.Log($"Cell: {cellX} : {cellZ}");
            }
        }

        for(int x = -_sizeX / 2 * 2; x <= _sizeX / 2 * 2; x += 2)
        {
            for (int z = -_sizeZ / 2 * 2; z <= _sizeZ / 2 * 2; z += 2)
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


                if(leftRoad)
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
                else if(!previousX)
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
                else if(!previousZ)
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

        for(int x = -_sizeX / 2 * 2 - 1; x <= _sizeX / 2 * 2 + 1; x += 2)
        {
            for (int z = -_sizeZ / 2 * 2 - 1; z <= _sizeZ / 2 * 2 + 1; z += 2)
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
}
