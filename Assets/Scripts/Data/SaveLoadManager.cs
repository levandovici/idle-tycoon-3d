using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class SaveLoadManager
{
    private const string _dataPath = "data.json";

    private const string _settingsPath = "settings.json";



    private static PlayerData _data = null;

    private static SettingsData _settings = null;



    public static PlayerData Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
        }
    }

    public static SettingsData Settings
    {
        get
        {
            return _settings;
        }

        private set
        {
            _settings = value;
        }
    }

    public static bool CanContinue
    {
        get
        {
            return _data != null;
        }
    }



    public static void Save()
    {
#if UNITY_EDITOR
        string dataPath = Path.Combine(Application.dataPath, _dataPath);

        string settingsPath = Path.Combine(Application.dataPath, _settingsPath);
#else
        string dataPath = Path.Combine(Application.persistentDataPath, _dataPath);

        string settingsPath = Path.Combine(Application.persistentDataPath, _settingsPath);
#endif


        Data.Terrain.Save();

        string dataJson = JsonUtility.ToJson(Data, true);

        File.WriteAllText(dataPath, dataJson);


        string settingsJson = JsonUtility.ToJson(Settings, true);

        File.WriteAllText(settingsPath, settingsJson);
    }

    public static void Load()
    {
#if UNITY_EDITOR
        string dataPath = Path.Combine(Application.dataPath, _dataPath);

        string settingsPath = Path.Combine(Application.dataPath, _settingsPath);
#else
        string dataPath = Path.Combine(Application.persistentDataPath, _dataPath);

        string settingsPath = Path.Combine(Application.persistentDataPath, _settingsPath);
#endif

        try
        {
            string dataJson = File.ReadAllText(dataPath);

            Data = JsonUtility.FromJson<PlayerData>(dataJson);

            Data.Terrain.Load();
        }
        catch(Exception e)
        {
            Debug.LogWarning(e.StackTrace);
        }

        try
        {
            string settingsJson = File.ReadAllText(settingsPath);

            Settings = JsonUtility.FromJson<SettingsData>(settingsJson);
        }
        catch (Exception e)
        {
            Settings = new SettingsData();

            Debug.LogWarning(e.StackTrace);
        }
    }
}
