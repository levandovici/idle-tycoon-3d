using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShortInfoPanel : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI _shortInfo;

    [SerializeField]
    private PricePanel _price;



    public void Setup(string text)
    {
        Setup(text, new Price());
    }

    public void Setup(string text, Price price)
    {
        _shortInfo.text = text;

        _price.Setup(price);
    }
}
