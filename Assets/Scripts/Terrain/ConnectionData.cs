using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ConnectionData
{
    [SerializeField]
    private Vector2Int _position;

    [SerializeField]
    private ConnectionType _type;



    public Vector2Int Position
    {
        get
        {
            return _position;
        }
    }

    public ConnectionType Type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }



    public ConnectionData(int positionX, int positionZ, ConnectionType type) : this(new Vector2Int(positionX, positionZ), type)
    {

    }

    public ConnectionData(Vector2Int position, ConnectionType type)
    {
        _position = position;

        Type = type;
    }
}

public enum ConnectionType
{
    None, Grass, Road,
}
