using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameplayUIManager _uiManager;

    [SerializeField]
    private TerrainController _terrain;

    [SerializeField]
    private CellBase _hovered = null;



    private void Awake()
    {
        SaveLoadManager.Load();

        _uiManager.Resources.Setup(SaveLoadManager.Data.Containers, SaveLoadManager.Data.Planks,
            SaveLoadManager.Data.Bricks, SaveLoadManager.Data.Money,
            SaveLoadManager.Data.Gold, SaveLoadManager.Data.Diamonds);

        _terrain.Setup(SaveLoadManager.Data.Terrain);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }

        Hover();
    }

    private void Click()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            Debug.Log(objectHit);

            if (objectHit.tag == "CellAdd")
            {
                CellAdd cell = objectHit.GetComponent<CellAdd>();

                _terrain.AddNewCell(cell.Position);
            }
        }
    }

    private void Hover()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            Debug.Log(objectHit);

            if (objectHit.tag == "Cell" || objectHit.tag == "CellAdd")
            {
                if (_hovered != null)
                {
                    _hovered.Unselect();
                }

                _hovered = objectHit.GetComponent<CellBase>();

                _hovered.Select();
            }
            else if (_hovered != null)
            {
                _hovered.Unselect();

                _hovered = null;
            }
        }
    }
}