using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [SerializeField]
    private Button _continue;

    [SerializeField]
    private Button _levels;

    [SerializeField]
    private Button _settings;



    public event Action OnContinue;

    public event Action OnLevels;

    public event Action OnSettings;



    public void Setup(bool canContinue)
    {
        _continue.gameObject.SetActive(canContinue);
    }



    private void Awake()
    {
        _continue.onClick.AddListener(ContinueEvent);

        _levels.onClick.AddListener(LevelsEvent);

        _settings.onClick.AddListener(SettingsEvent);
    }

    private void OnDestroy()
    {
        _continue.onClick.RemoveListener(ContinueEvent);

        _levels.onClick.RemoveListener(LevelsEvent);

        _settings.onClick.RemoveListener(SettingsEvent);
    }



    private void ContinueEvent()
    {
        OnContinue?.Invoke();
    }

    private void LevelsEvent()
    {
        OnLevels?.Invoke();
    }

    private void SettingsEvent()
    {
        OnSettings?.Invoke();
    }
}
