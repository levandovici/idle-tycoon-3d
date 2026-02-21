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



    public void Setup(int containers, int planks, int bricks, int money, int gold, int diamonds)
    {
        _containers.text = $"{containers}";

        _planks.text = $"{planks}";

        _bricks.text = $"{bricks}";

        _money.text = $"{money}";

        _gold.text = $"{gold}";

        _diamonds.text = $"{diamonds}";
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
}
