using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI _containers;

    [SerializeField]
    private TextMeshProUGUI _planks;

    [SerializeField]
    private TextMeshProUGUI _bricks;

    [SerializeField]
    private TextMeshProUGUI _money;

    [SerializeField]
    private TextMeshProUGUI _gold;

    [SerializeField]
    private TextMeshProUGUI _diamonds;

    [SerializeField]
    private Button _settings;



    public event Action OnSettings;



    private void Awake()
    {
        _settings.onClick.AddListener(SettingsEvent);
    }

    private void OnDestroy()
    {
        _settings.onClick.RemoveListener(SettingsEvent);
    }



    public void Setup(Price price)
    {
        _containers.text = $"{price.Containers}";

        _planks.text = $"{price.Planks}";

        _bricks.text = $"{price.Bricks}";

        _money.text = $"{price.Money}";

        _gold.text = $"{price.Gold}";

        _diamonds.text = $"{price.Diamonds}";
    }



    public void SetupContainers(int count)
    {
        _containers.text = $"{count}";
    }

    public void SetupPlanks(int count)
    {
        _planks.text = $"{count}";
    }

    public void SetupBricks(int count)
    {
        _bricks.text = $"{count}";
    }

    public void SetupMoney(int count)
    {
        _money.text = $"{count}";
    }

    public void SetupGold(int count)
    {
        _gold.text = $"{count}";
    }

    public void SetupDiamonds(int count)
    {
        _diamonds.text = $"{count}";
    }



    private void SettingsEvent()
    {
        SoundManager.Instance.Play(ESfx.Click);

        OnSettings?.Invoke();
    }
}
