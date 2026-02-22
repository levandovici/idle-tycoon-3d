using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    [SerializeField]
    private Selection _selection;



    protected Selection Selection
    {
        get
        {
            return _selection;
        }
    }



    public virtual void Select()
    {
        Selection.Select();
    }

    public virtual void Unselect()
    {
        Selection.Unselect();
    }
}
