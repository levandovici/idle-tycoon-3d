using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField]
    private int _containers = 0;

    [SerializeField]
    private int _planks = 0;

    [SerializeField]
    private int _bricks = 0;

    [SerializeField]
    private int _money = 0;

    [SerializeField]
    private int _gold = 0;

    [SerializeField]
    private int _diamonds = 0;

    [SerializeField]
    private TerrainData _terrain = new TerrainData(3, 3);



    public int Containers
    {
        get
        {
            return _containers;
        }

        set
        {
            _containers = value;
        }
    }

    public int Planks
    {
        get
        {
            return _planks;
        }

        set
        {
            _planks = value;
        }
    }

    public int Bricks
    {
        get
        {
            return _bricks;
        }

        set
        {
            _bricks = value;
        }
    }

    public int Money
    {
        get
        {
            return _money;
        }

        set
        {
            _money = value;
        }
    }

    public int Gold
    {
        get
        {
            return _gold;
        }

        set
        {
            _gold = value;
        }
    }

    public int Diamonds
    {
        get
        {
            return _diamonds;
        }

        set
        {
            _diamonds = value;
        }
    }

    public TerrainData Terrain
    {
        get
        {
            return _terrain;
        }

        set
        {
            _terrain = value;
        }
    }



    public PlayerData() : this(200, 500, 300, 600, 50, 10)
    {

    }

    public PlayerData(int containers, int planks, int bricks, int money, int gold, int diamonds) : 
        this(containers, planks, bricks, money, gold, diamonds, new TerrainData(3, 3))
    {

    }

    public PlayerData(int containers, int planks, int bricks, int money, int gold, int diamonds, TerrainData terrain)
    {
        Containers = containers;

        Planks = planks;

        Bricks = bricks;

        Money = money;

        Gold = gold;

        Diamonds = diamonds;

        Terrain = terrain;
    }
}
