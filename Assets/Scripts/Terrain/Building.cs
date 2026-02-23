using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private Price _production;

    [SerializeField]
    private float _productionDelay;


    [SerializeField]
    private float _generatedContainers = 0f;

    [SerializeField]
    private float _generatedPlanks = 0f;

    [SerializeField]
    private float _generatedBricks = 0f;

    [SerializeField]
    private float _generatedMoney = 0f;

    [SerializeField]
    private float _generatedGold = 0f;

    [SerializeField]
    private float _generatedDiamonds = 0f;



    public void Setup(Price production, float productionDelay)
    {
        _production = production;

        _productionDelay = productionDelay;
    }



    private void Update()
    {
        _generatedContainers += _production.Containers / _productionDelay * Time.deltaTime;

        _generatedPlanks += _production.Planks / _productionDelay * Time.deltaTime;

        _generatedBricks += _production.Bricks / _productionDelay * Time.deltaTime;

        _generatedMoney += _production.Money / _productionDelay * Time.deltaTime;

        _generatedGold += _production.Gold / _productionDelay * Time.deltaTime;

        _generatedDiamonds += _production.Diamonds / _productionDelay * Time.deltaTime;


        if(_generatedContainers >= 1f)
        {
            _generatedContainers--;

            SaveLoadManager.Data.Containers++;
        }

        if (_generatedPlanks >= 1f)
        {
            _generatedPlanks--;

            SaveLoadManager.Data.Planks++;
        }

        if (_generatedBricks >= 1f)
        {
            _generatedBricks--;

            SaveLoadManager.Data.Bricks++;
        }

        if (_generatedMoney >= 1f)
        {
            _generatedMoney--;

            SaveLoadManager.Data.Money++;
        }

        if (_generatedGold >= 1f)
        {
            _generatedGold--;

            SaveLoadManager.Data.Gold++;
        }

        if (_generatedDiamonds >= 1f)
        {
            _generatedDiamonds--;

            SaveLoadManager.Data.Diamonds++;
        }
    }
}
