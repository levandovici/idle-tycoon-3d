using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField]
    private ResourcesPanel _resources;

    [SerializeField]
    private ShortInfoPanel _shortInfo;

    [SerializeField]
    private InformationPanel _information;

    [SerializeField]
    private GameplaySettingsPanel _settings;

    [SerializeField]
    private EGameplayWindow _window;



    public event Action<EGameplayWindow> OnWindowChanged;



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

    public GameplaySettingsPanel Settings
    {
        get
        {
            return _settings;
        }
    }



    private void Awake()
    {
        _resources.Open();

        _shortInfo.Close();

        _information.Close();

        _settings.Close();
    }

    private void Start()
    {
        Change(EGameplayWindow.Gameplay);
    }



    public void Setup(EGameplayWindow window)
    {
        Change(window);
    }



    private void Change(EGameplayWindow window)
    {
        if (_window == window)
            return;

        Close(_window);

        Open(window);

        _window = window;

        OnWindowChanged?.Invoke(window);
    }

    private void Open(EGameplayWindow window)
    {
        switch(window)
        {
            case EGameplayWindow.Gameplay:
                _resources.Open();
                break;

            case EGameplayWindow.Settings:
                _settings.Open();
                break;
        }
    }

    private void Close(EGameplayWindow window)
    {
        switch (window)
        {
            case EGameplayWindow.Gameplay:
                _resources.Close();
                break;

            case EGameplayWindow.Settings:
                _settings.Close();
                break;
        }
    }
}

public enum EGameplayWindow
{
    None, Gameplay, Settings
}