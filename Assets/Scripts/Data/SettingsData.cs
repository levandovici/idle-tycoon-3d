using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SettingsData
{
    [SerializeField]
    private float _music = 0.5f;

    [SerializeField]
    private float _sfx = 0.5f;



    public float Music
    {
        get
        {
            return _music;
        }

        set
        {
            _music = value;
        }
    }

    public float Sfx
    {
        get
        {
            return _sfx;
        }

        set
        {
            _sfx = value;
        }
    }



    public SettingsData() : this(0.5f, 0.5f)
    {

    }

    public SettingsData(float music, float sfx)
    {
        Music = music;

        Sfx = sfx;
    }
}
