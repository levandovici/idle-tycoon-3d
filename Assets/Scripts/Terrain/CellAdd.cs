using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAdd : CellBase
{
    [SerializeField]
    private Vector2Int _position;



    public Vector2Int Position
    {
        get
        {
            return _position;
        }

        private set
        {
            _position = value;
        }
    }



    public void Setup(Vector2Int position)
    {
        Position = position;
    }
}
