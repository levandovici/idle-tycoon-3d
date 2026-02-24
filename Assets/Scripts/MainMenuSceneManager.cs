using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuUIManager _uiManager;



    private void Awake()
    {
        SaveLoadManager.Load();

        _uiManager.OnWindowChanged += OpenEvent;


        _uiManager.MainMenu.OnContinue += Continue;

        _uiManager.MainMenu.OnLevels += OpenLevels;

        _uiManager.MainMenu.OnSettings += OpenSettings;


        _uiManager.Levels.OnEasy += StartEasy;

        _uiManager.Levels.OnMedium += StartMedium;

        _uiManager.Levels.OnHard += StartHard;

        _uiManager.Levels.OnBack += OpenMainMenu;


        _uiManager.Settings.OnMusicChanged += MusicChangedEvent;

        _uiManager.Settings.OnSfxChanged += SfxChangedEvent;

        _uiManager.Settings.OnBack += OpenMainMenu;
    }

    private void OnDestroy()
    {
        _uiManager.OnWindowChanged -= OpenEvent;


        _uiManager.MainMenu.OnContinue -= Continue;

        _uiManager.MainMenu.OnLevels -= OpenLevels;

        _uiManager.MainMenu.OnSettings -= OpenSettings;


        _uiManager.Levels.OnEasy -= StartEasy;

        _uiManager.Levels.OnMedium -= StartMedium;

        _uiManager.Levels.OnHard -= StartHard;

        _uiManager.Levels.OnBack -= OpenMainMenu;


        _uiManager.Settings.OnMusicChanged -= MusicChangedEvent;

        _uiManager.Settings.OnSfxChanged -= SfxChangedEvent;

        _uiManager.Settings.OnBack -= OpenMainMenu;
    }

    private void OnApplicationQuit()
    {
        SaveLoadManager.Save();
    }



    private void Continue()
    {
        LoadingSceneManager.LoadingSceneIndex = 2;

        SceneManager.LoadScene(0);
    }

    private void StartEasy()
    {
        SaveLoadManager.Data = new PlayerData(ELevel.Easy);

        Continue();
    }

    private void StartMedium()
    {
        SaveLoadManager.Data = new PlayerData(ELevel.Medium);

        Continue();
    }

    private void StartHard()
    {
        SaveLoadManager.Data = new PlayerData(ELevel.Hard);

        Continue();
    }

    private void OpenMainMenu()
    {
        _uiManager.Setup(EMainMenuWindow.MainMenu);
    }

    private void OpenLevels()
    {
        _uiManager.Setup(EMainMenuWindow.Levels);
    }

    private void OpenSettings()
    {
        _uiManager.Setup(EMainMenuWindow.Settings);
    }



    private void MusicChangedEvent(float value)
    {
        SaveLoadManager.Settings.Music = value;
    }

    private void SfxChangedEvent(float value)
    {
        SaveLoadManager.Settings.Sfx = value;
    }


    private void OpenEvent(EMainMenuWindow window)
    {
        switch(window)
        {
            case EMainMenuWindow.MainMenu:
                OpenMainMenuEvent();
                break;

            case EMainMenuWindow.Levels:
                OpenLevelsEvent();
                break;

            case EMainMenuWindow.Settings:
                OpenSettingsEvent();
                break;
        }
    }

    private void OpenMainMenuEvent()
    {
        _uiManager.MainMenu.Setup(SaveLoadManager.CanContinue);
    }

    private void OpenLevelsEvent()
    {

    }

    private void OpenSettingsEvent()
    {
        _uiManager.Settings.Setup(SaveLoadManager.Settings.Music, SaveLoadManager.Settings.Sfx);
    }
}
