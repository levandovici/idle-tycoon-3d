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

        RefreshResources();

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



    private void RefreshResources()
    {
        _uiManager.Resources.Setup(SaveLoadManager.Data.Resources);
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

                if (SaveLoadManager.Data.TryPay(new Price(gold: 5)))
                {
                    _terrain.AddNewCell(cell.Position);

                    RefreshResources();
                }
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

            if (objectHit.tag == "Cell")
            {
                HoverCell(objectHit, objectHit.name);
            }
            else if(objectHit.tag == "CellAdd")
            {
                HoverCell(objectHit, $"New Terrain", new Price(gold:5));
            }
            else if (_hovered != null)
            {
                _hovered.Unselect();

                _hovered = null;

                _uiManager.ShortInfo.Close();
            }
        }
        else if (_hovered != null)
        {
            _hovered.Unselect();

            _hovered = null;

            _uiManager.ShortInfo.Close();
        }

        void HoverCell(Transform objectHit, string information, Price price = null)
        {
            if (_hovered != null)
            {
                _hovered.Unselect();

                _uiManager.ShortInfo.Close();
            }

            _hovered = objectHit.GetComponent<CellBase>();

            _hovered.Select();

            if (price == null)
            {
                _uiManager.ShortInfo.Setup(information);
            }
            else
            {
                _uiManager.ShortInfo.Setup(information, price);
            }

            _uiManager.ShortInfo.Open();
        }
    }
}