using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField]
    private ResourcesPanel _resources;

    [SerializeField]
    private ShortInfoPanel _shortInfo;



    public ResourcesPanel Resources
    {
        get
        {
            return _resources;
        }
    }

    public ShortInfoPanel ShortInfo
    {
        get
        {
            return _shortInfo;
        }
    }
}
