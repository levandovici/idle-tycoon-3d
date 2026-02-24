using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuPanel _mainMenu;

    [SerializeField]
    private LevelsPanel _levels;

    [SerializeField]
    private SettingsPanel _settings;

    [SerializeField]
    private EMainMenuWindow _window;



    public event Action<EMainMenuWindow> OnWindowChanged;



    public MainMenuPanel MainMenu => _mainMenu;

    public LevelsPanel Levels => _levels;

    public SettingsPanel Settings => _settings;



    public void Setup(EMainMenuWindow window)
    {
        Change(window);
    }



    private void Awake()
    {
        _mainMenu.Open();

        _levels.Close();

        _settings.Close();
    }

    private void Start()
    {
        Change(EMainMenuWindow.MainMenu);
    }



    private void Change(EMainMenuWindow window)
    {
        if (_window == window)
            return;

        Close(_window);

        Open(window);

        _window = window;

        OnWindowChanged?.Invoke(window);
    }


    private void Open(EMainMenuWindow window)
    {
        switch (window)
        {
            case EMainMenuWindow.MainMenu:
                _mainMenu.Open();
                break;

            case EMainMenuWindow.Levels:
                _levels.Open();
                break;

            case EMainMenuWindow.Settings:
                _settings.Open();
                break;
        }
    }

    private void Close(EMainMenuWindow window)
    {
        switch(window)
        {
            case EMainMenuWindow.MainMenu:
                _mainMenu.Close();
                break;

            case EMainMenuWindow.Levels:
                _levels.Close();
                break;

            case EMainMenuWindow.Settings:
                _settings.Close();
                break;
        }
    }
}

public enum EMainMenuWindow
{
    None, MainMenu, Levels, Settings
}
