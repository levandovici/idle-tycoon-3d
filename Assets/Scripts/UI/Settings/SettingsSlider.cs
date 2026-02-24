using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _sliderText;

    [SerializeField]
    private Slider _slider;



    public event Action<float> OnValueChanged;



    public void Setup(float progress)
    {
        _sliderText.text = $"{Mathf.FloorToInt(progress * 100f)}";

        _slider.value = progress;
    }



    private void Awake()
    {
        _slider.onValueChanged.AddListener(ValueChangedEvent);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(ValueChangedEvent);
    }



    private void ValueChangedEvent(float value)
    {
        _sliderText.text = $"{Mathf.FloorToInt(value * 100f)}";

        OnValueChanged?.Invoke(value);
    }
}
