using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CellData
{
    [SerializeField]
    private Vector2Int _position;

    [SerializeField]
    private EBuilding _building = EBuilding.None;

    [SerializeField]
    private int _level = 1;



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

    public EBuilding Building
    {
        get
        {
            return _building;
        }

        set
        {
            _building = value;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }

        set
        {
            _level = value;
        }
    }



    public CellData(int positionX, int positionZ) : this(new Vector2Int(positionX, positionZ))
    {

    }

    public CellData(Vector2Int position, EBuilding building = EBuilding.None, int level = 1)
    {
        _position = position;

        Building = building;

        Level = level;
    }
}

public enum EBuilding
{
    None, Factory, House, Production, Warehouse
}
