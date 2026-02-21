using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private CellData _data;

    [SerializeField]
    private Selection _selection;



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

    public Selection Selection
    {
        get
        {
            return _selection;
        }
    }



    public void Setup(CellData data)
    {
        Data = data;
    }
}
