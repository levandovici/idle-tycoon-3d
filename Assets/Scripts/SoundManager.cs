using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;



    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }



    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private AudioSource _sfx;

    [SerializeField]
    private AudioClip _click;

    [SerializeField]
    private AudioClip _error;



    public void Setup(float music, float sfx)
    {
        SetupMusic(music);

        SetupSfx(sfx);
    }



    public void Play(EMusic music)
    {
        string path;

        switch (music)
        {
            case EMusic.Loading:
                path = "Music/Loading";
                break;

            case EMusic.MainMenu:
                path = "Music/MainMenu";
                break;

            case EMusic.Gameplay:
                path = "Music/Gameplay";
                break;

            default:
                throw new NotImplementedException();
        }

        AudioClip audio = Resources.Load<AudioClip>(path);

        _music.clip = audio;

        _music.Play();

        Resources.UnloadUnusedAssets();
    }

    public void Play(ESfx sfx)
    {
        switch(sfx)
        {
            case ESfx.Click:
                _sfx.PlayOneShot(_click);
                break;

            case ESfx.Error:
                _sfx.PlayOneShot(_error);
                break;
        }
    }


    private void SetupMusic(float music)
    {
        _music.volume = music;
    }

    private void SetupSfx(float sfx)
    {
        _sfx.volume = sfx;
    }



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}

public enum EMusic
{
    Loading, MainMenu, Gameplay
}

public enum ESfx
{
    Click, Error
}
