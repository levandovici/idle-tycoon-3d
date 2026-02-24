using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : UIPanel
{
    [SerializeField]
    private SettingsSlider _music;

    [SerializeField]
    private SettingsSlider _sfx;

    [SerializeField]
    private Button _back;



    public event Action<float> OnMusicChanged;

    public event Action<float> OnSfxChanged;

    public event Action OnBack;



    public void Setup(float music, float sfx)
    {
        SetupMusic(music);

        SetupSfx(sfx);
    }

    public void SetupMusic(float volume)
    {
        _music.Setup(volume);
    }

    public void SetupSfx(float volume)
    {
        _sfx.Setup(volume);
    }



    protected virtual void Awake()
    {
        _music.OnValueChanged += MusicChangedEvent;

        _sfx.OnValueChanged += SfxChangedEvent;

        _back.onClick.AddListener(BackEvent);
    }

    protected virtual void OnDestroy()
    {
        _music.OnValueChanged -= MusicChangedEvent;

        _sfx.OnValueChanged -= SfxChangedEvent;

        _back.onClick.RemoveListener(BackEvent);
    }



    private void MusicChangedEvent(float volume)
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnMusicChanged?.Invoke(volume);
    }

    private void SfxChangedEvent(float volume)
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnSfxChanged?.Invoke(volume);
    }

    private void BackEvent()
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnBack?.Invoke();
    }
}
