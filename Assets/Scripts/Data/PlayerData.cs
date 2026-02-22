using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField]
    private Price _resources;

    [SerializeField]
    private TerrainData _terrain = new TerrainData(3, 3);



    public Price Resources
    {
        get
        {
            return _resources;
        }

        private set
        {
            _resources = value;
        }
    }

    public int Containers
    {
        get
        {
            return _resources.Containers;
        }

        set
        {
            _resources.Containers = value;
        }
    }

    public int Planks
    {
        get
        {
            return _resources.Planks;
        }

        set
        {
            _resources.Planks = value;
        }
    }

    public int Bricks
    {
        get
        {
            return _resources.Bricks;
        }

        set
        {
            _resources.Bricks = value;
        }
    }

    public int Money
    {
        get
        {
            return _resources.Money;
        }

        set
        {
            _resources.Money = value;
        }
    }

    public int Gold
    {
        get
        {
            return _resources.Gold;
        }

        set
        {
            _resources.Gold = value;
        }
    }

    public int Diamonds
    {
        get
        {
            return _resources.Diamonds;
        }

        set
        {
            _resources.Diamonds = value;
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
        Resources = new Price(containers, planks, bricks, money, gold, diamonds);

        Terrain = terrain;
    }



    public bool TryPay(Price price)
    {
        if(_resources >= price)
        {
            _resources -= price;

            return true;
        }

        return false;
    }
}
