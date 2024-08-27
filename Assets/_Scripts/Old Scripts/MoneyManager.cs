using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;

public class MoneyManager : MonoBehaviour
{
    public static event Action UpdateBuyMode;
    private Data gameData;
    public TextMeshProUGUI mText;
    public List<Vector2> moneyOverTime;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>()._data;
        MultiplierManager.AddMoney += Add;
        GameData.loadGame += loadData;
        loadData();
        //StartCoroutine("TrackMoneyOverTime");
    }

    // Update is called once per frame
    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        TextManager.displayValue(mText, "$", gameData.Money);
    }

    private void Add(double m)
    {
        gameData.Money += m;
        gameData.RunMoney += m;
        gameData.LifetimeMoney += m;
        TextManager.displayValue(mText, "$", gameData.Money);
        UpdateBuyMode?.Invoke();
    }
    private void SubtractMoney(double m)
    {
        gameData.Money -= m;
        TextManager.displayValue(mText, "$", gameData.Money);
        UpdateBuyMode?.Invoke();
    }
}
