using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : CellBase
{
    [SerializeField]
    private CellData _data;



    public CellData Data
    {
        get
        {
            return _data;
        }

        private set
        {
            _data = value;
        }
    }



    public void Setup(CellData data)
    {
        Data = data;
    }
}
