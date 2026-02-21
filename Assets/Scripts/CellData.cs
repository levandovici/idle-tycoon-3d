using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CellData
{
    [SerializeField]
    private Vector2Int _position;



    public Vector2Int Position
    {
        get
        {
            return _position;
        }
    }

    public CellData(int positionX, int positionZ) : this(new Vector2Int(positionX, positionZ))
    {

    }

    public CellData(Vector2Int position)
    {
        _position = position;
    }
}
