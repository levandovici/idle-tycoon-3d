using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField]
    private ResourcesPanel _resources;

    [SerializeField]
    private ShortInfoPanel _shortInfo;

    [SerializeField]
    private InformationPanel _information;



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

    public InformationPanel Information
    {
        get
        {
            return _information;
        }
    }
}
