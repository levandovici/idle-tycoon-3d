using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : UIPanel
{
    [SerializeField]
    private TextMeshProUGUI _sliderText;

    [SerializeField]
    private Slider _slider;



    public void Setup(float progress)
    {
        _sliderText.text = $"{Mathf.FloorToInt(progress * 100f)} %";

        _slider.value = progress;
    }
}
