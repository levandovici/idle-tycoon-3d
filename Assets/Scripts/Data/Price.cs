using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class Price
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



    public Price(int containers = 0, int planks = 0, int bricks = 0, int money = 0, int gold = 0, int diamonds = 0)
    {
        Containers = containers;

        Planks = planks;

        Bricks = bricks;

        Money = money;

        Gold = gold;

        Diamonds = diamonds;
    }



    public Price Clone()
    {
        return new Price(Containers, Planks, Bricks, Money, Gold, Diamonds);
    }



    public static Price operator +(Price a, Price b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();

        return new Price(a.Containers + b.Containers, a.Planks + b.Planks, a.Bricks + b.Bricks, a.Money + b.Money, a.Gold + b.Gold, a.Diamonds + b.Diamonds);
    }

    public static Price operator -(Price a, Price b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();

        return new Price(a.Containers - b.Containers, a.Planks - b.Planks, a.Bricks - b.Bricks, a.Money - b.Money, a.Gold - b.Gold, a.Diamonds - b.Diamonds);
    }



    public static bool operator <(Price a, Price b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();

        return a.Containers < b.Containers && a.Planks < b.Planks && a.Bricks < b.Bricks && a.Money < b.Money && a.Gold < b.Gold && a.Diamonds < b.Diamonds;
    }

    public static bool operator >(Price a, Price b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();

        return a.Containers > b.Containers && a.Planks > b.Planks && a.Bricks > b.Bricks && a.Money > b.Money && a.Gold > b.Gold && a.Diamonds > b.Diamonds;
    }

    public static bool operator <=(Price a, Price b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();

        return a.Containers <= b.Containers && a.Planks <= b.Planks && a.Bricks <= b.Bricks && a.Money <= b.Money && a.Gold <= b.Gold && a.Diamonds <= b.Diamonds;
    }

    public static bool operator >=(Price a, Price b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();

        return a.Containers >= b.Containers && a.Planks >= b.Planks && a.Bricks >= b.Bricks && a.Money >= b.Money && a.Gold >= b.Gold && a.Diamonds >= b.Diamonds;
    }



    public static bool operator ==(Price a, Price b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Containers == b.Containers
            && a.Planks == b.Planks
            && a.Bricks == b.Bricks
            && a.Money == b.Money
            && a.Gold == b.Gold
            && a.Diamonds == b.Diamonds;
    }

    public static bool operator !=(Price a, Price b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Price other)
            return this == other;

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Containers, Planks, Bricks, Money, Gold, Diamonds);
    }
}
