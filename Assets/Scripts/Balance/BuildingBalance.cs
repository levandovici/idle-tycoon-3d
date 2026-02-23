using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingBalance
{
    private Price _buy;

    private BuildingLevel[] _levels;



    public Price Buy
    {
        get
        {
            return _buy;
        }

        private set
        {
            _buy = value;
        }
    }

    public int MaxLevel
    {
        get
        {
            return _levels.Length;
        }
    }

    
    
    public Price Upgrade(int level)
    {
        return _levels[level - 1].upgrade;
    }

    public Price Production(int level)
    {
        return _levels[level - 1].production;
    }

    public float ProductionDelay(int level)
    {
        return _levels[level - 1].productionDelay;
    }



    public BuildingBalance(Price buy, params BuildingLevel[] levels)
    {
        Buy = buy;

        _levels = levels;
    }
}

public class BuildingLevel
{
    public Price upgrade;

    public Price production;

    public float productionDelay;



    public BuildingLevel(Price upgrade, Price production, float productionDelay)
    {
        this.upgrade = upgrade;

        this.production = production;

        this.productionDelay = productionDelay;
    }
}
