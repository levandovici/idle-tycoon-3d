using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanel : UIPanel
{
    [SerializeField]
    private Button _easy;

    [SerializeField]
    private Button _medium;

    [SerializeField]
    private Button _hard;

    [SerializeField]
    private Button _back;



    public event Action OnEasy;

    public event Action OnMedium;

    public event Action OnHard;

    public event Action OnBack;



    private void Awake()
    {
        _easy.onClick.AddListener(EasyEvent);

        _medium.onClick.AddListener(MediumEvent);

        _hard.onClick.AddListener(HardEvent);

        _back.onClick.AddListener(BackEvent);
    }

    private void OnDestroy()
    {
        _easy.onClick.RemoveListener(EasyEvent);

        _medium.onClick.RemoveListener(MediumEvent);

        _hard.onClick.RemoveListener(HardEvent);

        _back.onClick.RemoveListener(BackEvent);
    }



    private void EasyEvent()
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnEasy?.Invoke();
    }

    private void MediumEvent()
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnMedium?.Invoke();
    }

    private void HardEvent()
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnHard?.Invoke();
    }

    private void BackEvent()
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnBack?.Invoke();
    }
}
