using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class SaveLoadManager
{
    private const string _path = "data.json";



    private static PlayerData _data = null;



    public static PlayerData Data
    {
        get
        {
            return _data;
        }
    }



    public static void Save()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, _path);
#else
        string path = Path.Combine(Application.persistentDataPath, _path);
#endif

        _data.Terrain.Save();

        string json = JsonUtility.ToJson(_data, true);

        File.WriteAllText(path, json);
    }

    public static void Load()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, _path);
#else
        string path = Path.Combine(Application.persistentDataPath, _path);
#endif

        try
        {
            string json = File.ReadAllText(path);

            _data = JsonUtility.FromJson<PlayerData>(json);

            _data.Terrain.Load();
        }
        catch(Exception e)
        {
            _data = new PlayerData();

            Debug.LogWarning(e.StackTrace);
        }
    }
}
