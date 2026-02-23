using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI _information;

    [SerializeField]
    private OptionButton[] _optionButtons = new OptionButton[4];


    [SerializeField]
    private PricePanel _price;

    [SerializeField]
    private OptionButton _actionButton;



    public void Setup(string text)
    {
        Setup(text, new Price());
    }

    public void Setup(string text, Price price = null, OptionSetup actionSetup = null, params OptionSetup[] optionsSetup)
    {
        _information.text = text;

        if (price == null)
        {
            _price.Setup(new Price());
        }
        else
        {
            _price.Setup(price);
        }

        _actionButton.button.gameObject.SetActive(actionSetup != null);

        _actionButton.button.onClick.RemoveAllListeners();

        if (actionSetup != null)
        {
            _actionButton.text.text = actionSetup.text;

            _actionButton.button.onClick.AddListener(() => actionSetup.action.Invoke());
        }

        if(optionsSetup.Length > _optionButtons.Length)
        {
            throw new ArgumentOutOfRangeException();
        }

        for (int i = 0; i < _optionButtons.Length; i++)
        {
            _optionButtons[i].button.gameObject.SetActive(optionsSetup.Length > i);

            _optionButtons[i].button.onClick.RemoveAllListeners();

            if (optionsSetup.Length > i)
            {
                _optionButtons[i].text.text = optionsSetup[i].text;

                int index = i;

                _optionButtons[i].button.onClick.AddListener(() => optionsSetup[index].action.Invoke());
            }
        }
    }
}



[Serializable]
public class OptionButton
{
    public Button button;

    public TextMeshProUGUI text;
}

[Serializable]
public class OptionSetup
{
    public string text;

    public Action action;



    public OptionSetup(string text, Action action)
    {
        this.text = text;

        this.action = action;
    }
}
