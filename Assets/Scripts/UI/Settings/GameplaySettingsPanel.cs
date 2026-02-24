using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettingsPanel : SettingsPanel
{
    [SerializeField]
    private Button _exit;



    public event Action OnExit;



    protected override void Awake()
    {
        base.Awake();

        _exit.onClick.AddListener(ExitEvent);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _exit.onClick.RemoveListener(ExitEvent);
    }



    private void ExitEvent()
    {
        OnExit?.Invoke();
    }
}
