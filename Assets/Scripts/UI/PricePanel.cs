using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PricePanel : UIPanel
{
    [SerializeField]
    private GameObject _containers;

    [SerializeField]
    private TextMeshProUGUI _containersText;

    [SerializeField]
    private GameObject _planks;

    [SerializeField]
    private TextMeshProUGUI _planksText;

    [SerializeField]
    private GameObject _bricks;

    [SerializeField]
    private TextMeshProUGUI _bricksText;

    [SerializeField]
    private GameObject _money;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    [SerializeField]
    private GameObject _gold;

    [SerializeField]
    private TextMeshProUGUI _goldText;

    [SerializeField]
    private GameObject _diamonds;

    [SerializeField]
    private TextMeshProUGUI _diamondsText;



    public void Setup(Price price)
    {
        _containers.SetActive(price.Containers > 0);

        _containersText.text = $"{price.Containers}";


        _planks.SetActive(price.Planks > 0);

        _planksText.text = $"{price.Planks}";


        _bricks.SetActive(price.Bricks > 0);

        _bricksText.text = $"{price.Bricks}";


        _money.SetActive(price.Money > 0);

        _moneyText.text = $"{price.Money}";


        _gold.SetActive(price.Gold > 0);

        _goldText.text = $"{price.Gold}";


        _diamonds.SetActive(price.Diamonds > 0);

        _diamondsText.text = $"{price.Diamonds}";
    }
}
