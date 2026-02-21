using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField]
    private ResourcesPanel _resources;



    public ResourcesPanel Resources
    {
        get
        {
            return _resources;
        }
    }
}
