using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinData
{
    [SerializeField]
    private Vector2Int _position;

    [SerializeField]
    private bool[] _joints = new bool[4];



    public Vector2Int Position
    {
        get
        {
            return _position;
        }
    }

    public bool Left
    {
        get
        {
            return _joints[0];
        }
    }

    public bool Right
    {
        get
        {
            return _joints[1];
        }
    }

    public bool Top
    {
        get
        {
            return _joints[2];
        }
    }

    public bool Bottom
    {
        get
        {
            return _joints[3];
        }
    }

    public JoinType Type
    {
        get
        {
            int count = 0;

            for (int i = 0; i < _joints.Length; i++)
            {
                if (_joints[i])
                {
                    count++;
                }
            }

            switch(count)
            {
                case 4:
                    return JoinType.RoadX;

                case 3:
                    return JoinType.RoadT;

                case 2:
                    if(Left && Right || Top && Bottom)
                    {
                        return JoinType.RoadI;
                    }
                    else
                    {
                        return JoinType.RoadL;
                    }

                case 1:
                    return JoinType.RoadU;

                case 0:
                    return JoinType.Grass;

                default:
                    return JoinType.None;
            }
        }
    }



    public JoinData(int positionX, int positionZ, bool left, bool right, bool top, bool bottom) : 
        this(new Vector2Int(positionX, positionZ), left, right, top, bottom)
    {

    }

    public JoinData(Vector2Int position, bool left, bool right, bool top, bool bottom)
    {
        _position = position;

        _joints = new bool[4];

        _joints[0] = left;

        _joints[1] = right;

        _joints[2] = top;

        _joints[3] = bottom;
    }
}

public enum JoinType
{
    None, RoadX, RoadT, RoadI, RoadL, RoadU, Grass
}
