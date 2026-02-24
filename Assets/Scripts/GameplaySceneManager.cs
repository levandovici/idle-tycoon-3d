using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameplayUIManager _uiManager;

    [SerializeField]
    private TerrainController _terrain;

    [SerializeField]
    private CellBase _hovered = null;

    [SerializeField]
    private Cell _selected = null;

    [SerializeField]
    private Price _resources = null;

    [SerializeField]
    private HumanSpawner _spawner;



    private void Awake()
    {
#if UNITY_EDITOR
        if(SaveLoadManager.Data == null)
        {
            SceneManager.LoadScene(0);
        }
#endif
        _resources = SaveLoadManager.Data.Resources.Clone();


        _terrain.Setup(SaveLoadManager.Data.Terrain);

        _terrain.Bounds(out float minX, out float maxX, out float minZ, out float maxZ);

        _spawner.Spawn(minX, maxX, minZ, maxZ, 30);


        _uiManager.OnWindowChanged += OpenEvent;


        _uiManager.Resources.OnSettings += OpenSettings;


        _uiManager.Settings.OnBack += OpenGameplay;

        _uiManager.Settings.OnExit += Exit;

        _uiManager.Settings.OnMusicChanged += MusicChangedEvent;

        _uiManager.Settings.OnSfxChanged += SfxChangedEvent;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Click();
        }

        Hover();

        if(SaveLoadManager.Data.Resources != _resources)
        {
            _resources = SaveLoadManager.Data.Resources.Clone();

            RefreshResources();
        }
    }



    private void Exit()
    {
        Save();

        LoadingSceneManager.LoadingSceneIndex = 1;

        SceneManager.LoadScene(0);
    }



    private void OpenGameplay()
    {
        _uiManager.Setup(EGameplayWindow.Gameplay);
    }

    private void OpenSettings()
    {
        _uiManager.Setup(EGameplayWindow.Settings);
    }



    private void MusicChangedEvent(float value)
    {
        SaveLoadManager.Settings.Music = value;
    }

    private void SfxChangedEvent(float value)
    {
        SaveLoadManager.Settings.Sfx = value;
    }



    private void OpenEvent(EGameplayWindow window)
    {
        switch(window)
        {
            case EGameplayWindow.Gameplay:
                OpenGameplayEvent();
                break;

            case EGameplayWindow.Settings:
                OpenSettingsEvent();
                break;
        }
    }

    private void OpenGameplayEvent()
    {
        RefreshResources();
    }

    private void OpenSettingsEvent()
    {
        _uiManager.Settings.Setup(SaveLoadManager.Settings.Music, SaveLoadManager.Settings.Sfx);
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

            if (objectHit.tag == "Cell")
            {
                Cell cell = objectHit.GetComponent<Cell>();

                ClickCell(cell);
            }
            else if (objectHit.tag == "CellAdd")
            {
                CellAdd cell = objectHit.GetComponent<CellAdd>();

                ClickCellAdd(cell);
            }
            else if (_selected != null)
            {
                _selected.Unselect();

                _selected = null;

                _uiManager.Information.Close();
            }
        }
        else if (_selected != null)
        {
            _selected.Unselect();

            _selected = null;

            _uiManager.Information.Close();
        }
    }

    private void ClickCell(Cell cell)
    {
        if(_selected != null)
        {
            _selected.Unselect();
        }

        _selected = cell;

        _selected.Select();

        _uiManager.Information.Open();

        switch (cell.Data.Building)
        {
            case EBuilding.Factory:
                if (CanUpgrade(EBuilding.Factory, cell.Data.Level, out Price factoryPrice))
                {
                    _uiManager.Information.Setup($"Factory\n[Level {cell.Data.Level}]", factoryPrice, 
                        new OptionSetup("Upgrade Factory", () => UpgradeEvent(cell.Data.Position, factoryPrice)));
                }
                else
                {
                    _uiManager.Information.Setup($"Factory\n[Max Level {cell.Data.Level}]");
                }

                break;

            case EBuilding.House:
                if (CanUpgrade(EBuilding.House, cell.Data.Level, out Price housePrice))
                {
                    _uiManager.Information.Setup($"House\n[Level {cell.Data.Level}]", housePrice,
                        new OptionSetup("Upgrade House", () => UpgradeEvent(cell.Data.Position, housePrice)));
                }
                else
                {
                    _uiManager.Information.Setup($"House\n[Max Level {cell.Data.Level}]");
                }

                break;

            case EBuilding.Production:
                if (CanUpgrade(EBuilding.Production, cell.Data.Level, out Price productionPrice))
                {
                    _uiManager.Information.Setup($"Production\n[Level {cell.Data.Level}]", productionPrice,
                        new OptionSetup("Upgrade Production", () => UpgradeEvent(cell.Data.Position, productionPrice)));
                }
                else
                {
                    _uiManager.Information.Setup($"Production\n[Max Level {cell.Data.Level}]");
                }

                break;

            case EBuilding.Warehouse:
                if (CanUpgrade(EBuilding.Warehouse, cell.Data.Level, out Price warehousePrice))
                {
                    _uiManager.Information.Setup($"Warehouse\n[Level {cell.Data.Level}]", warehousePrice,
                        new OptionSetup("Upgrade Warehouse", () => UpgradeEvent(cell.Data.Position, warehousePrice)));
                }
                else
                {
                    _uiManager.Information.Setup($"Warehouse\n[Max Level {cell.Data.Level}]");
                }

                break;

            default:
                _uiManager.Information.Setup("Empty Terrain", 
                    optionsSetup: new OptionSetup[4]
                    {
                        new OptionSetup("Build Factory", () => BuildEvent("Build Factory", cell.Data.Position, EBuilding.Factory)),
                        new OptionSetup("Build House", () => BuildEvent("Build House", cell.Data.Position, EBuilding.House)),
                        new OptionSetup("Build Production", () => BuildEvent("Build Production",  cell.Data.Position, EBuilding.Production)),
                        new OptionSetup("Build Warehouse", () => BuildEvent("Build Warehouse", cell.Data.Position, EBuilding.Warehouse))
                    });
                break;
        }
    }

    private void ClickCellAdd(CellAdd cell)
    {
        if (SaveLoadManager.Data.TryPay(Balance.NewTerrain * (int)SaveLoadManager.Data.Level))
        {
            _terrain.AddNewCell(cell.Position);

            RefreshResources();
        }

        if (_selected != null)
        {
            _selected.Unselect();

            _selected = null;

            _uiManager.Information.Close();
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
                Cell cell = objectHit.GetComponent<Cell>();

                string cellName;

                switch(cell.Data.Building)
                {
                    case EBuilding.Factory:
                        cellName = $"Factory\n[Level {cell.Data.Level}]";
                        break;

                    case EBuilding.House:
                        cellName = $"House\n[Level {cell.Data.Level}]";
                        break;

                    case EBuilding.Production:
                        cellName = $"Production\n[Level {cell.Data.Level}]";
                        break;

                    case EBuilding.Warehouse:
                        cellName = $"Warehouse\n[Level {cell.Data.Level}]";
                        break;

                    default:
                        cellName = "Empty Terrain";
                        break;
                }

                HoverCell(objectHit, cellName);
            }
            else if(objectHit.tag == "CellAdd")
            {
                HoverCell(objectHit, $"New Terrain", Balance.NewTerrain * (int)SaveLoadManager.Data.Level);
            }
            else if (_hovered != null)
            {
                if (_hovered != _selected)
                {
                    _hovered.Unselect();
                }

                _hovered = null;

                _uiManager.ShortInfo.Close();
            }
        }
        else if (_hovered != null)
        {
            if (_hovered != _selected)
            {
                _hovered.Unselect();
            }

            _hovered = null;

            _uiManager.ShortInfo.Close();
        }

        void HoverCell(Transform objectHit, string information, Price price = null)
        {
            if (_hovered != null)
            {
                if (_hovered != _selected)
                {
                    _hovered.Unselect();
                }

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



    private void BuildEvent(string text, Vector2Int position, EBuilding building)
    {
        _uiManager.Information.Open();

        Price price;

        switch(building)
        {
            case EBuilding.Factory:
                price = Balance.Factory.Buy * (int)SaveLoadManager.Data.Level;
                break;

            case EBuilding.House:
                price = Balance.House.Buy * (int)SaveLoadManager.Data.Level;
                break;

            case EBuilding.Production:
                price = Balance.Production.Buy * (int)SaveLoadManager.Data.Level;
                break;

            case EBuilding.Warehouse:
                price = Balance.Warehouse.Buy * (int)SaveLoadManager.Data.Level;
                break;

            default:
                throw new NotImplementedException();
        }

        _uiManager.Information.Setup(text, price, new OptionSetup("Build", () => BuildEvent(position, building, price)));
    }

    private void BuildEvent(Vector2Int position, EBuilding building, Price price)
    {
        if (SaveLoadManager.Data.TryPay(price))
        {
            _uiManager.Information.Close();

            _terrain.BuildOnCell(position, building);

            RefreshResources();
        }
    }

    private void UpgradeEvent(Vector2Int position, Price price)
    {
        if(SaveLoadManager.Data.TryPay(price))
        {
            _uiManager.Information.Close();

            _terrain.UpgradeCell(position);

            RefreshResources();
        }
    }

    private bool CanUpgrade(EBuilding building, int level, out Price price)
    {
        switch(building)
        {
            case EBuilding.Factory:
                price = Balance.Factory.Upgrade(level) * (int)SaveLoadManager.Data.Level;

                return level < Balance.Factory.MaxLevel;

            case EBuilding.House:
                price = Balance.House.Upgrade(level) * (int)SaveLoadManager.Data.Level;

                return level < Balance.House.MaxLevel;

            case EBuilding.Production:
                price = Balance.Production.Upgrade(level) * (int)SaveLoadManager.Data.Level;

                return level < Balance.Production.MaxLevel;

            case EBuilding.Warehouse:
                price = Balance.Warehouse.Upgrade(level) * (int)SaveLoadManager.Data.Level;

                return level < Balance.Warehouse.MaxLevel;

            case EBuilding.None:
                price = null;

                return false;

            default:
                throw new NotImplementedException();
        }
    }



    [ContextMenu("Save")]
    private void Save()
    {
        SaveLoadManager.Save();
    }
}