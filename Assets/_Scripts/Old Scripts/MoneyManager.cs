using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;

public class MoneyManager : MonoBehaviour
{
    public static event Action PrestigeReset;
    public static event Action UpdateBuyMode;
    private Data gameData;
    public TextMeshProUGUI mText;
    public TextMeshProUGUI pText;
    public TextMeshProUGUI pTotalText;
    public TextMeshProUGUI pExplanationText;
    public List<Vector2> moneyOverTime;
    private int seconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>()._data;
        MultiplierManager.AddMoney += Add;
        UpgradeManager.SubtractMoney += SubtractMoney;
        PrestigeUpgradeManager.SubtractPrestige += SubtractPrestige;
        PrestigeUpgradeManager.UpdateIncomeBoost += UpdateExplanation;
        GameData.loadGame += loadData;
        loadData();
        //StartCoroutine("TrackMoneyOverTime");
    }

    // Update is called once per frame
    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        TextManager.displayValue(mText, "$", gameData.Money);
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
        TextManager.displayValue(pTotalText, gameData.TotalPrestige, "p");
        UpdateExplanation();
    }

    void UpdateExplanation()
    {
        pExplanationText.SetText("Your " + TextManager.convertNum(gameData.TotalPrestige) + "p is providing a " + TextManager.convertNum(gameData.TotalPrestige * (10 + (gameData.PrestigeUpgradeCount[1] * 10 + ((gameData.PrestigeUpgradeCount[1]) * (gameData.PrestigeUpgradeCount[1] + 1) / 2)))) + "% boost to your income!");
    }

    private void Add(double m)
    {
        gameData.Money += m;
        gameData.RunMoney += m;
        gameData.LifetimeMoney += m;
        gameData.NextPrestige = Math.Round((150 * (Math.Sqrt(gameData.RunMoney / Mathf.Pow(10, 8)))));
        TextManager.displayValue(mText, "$", gameData.Money);
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
        UpdateBuyMode?.Invoke();
    }
    private void SubtractMoney(double m)
    {
        gameData.Money -= m;
        TextManager.displayValue(mText, "$", gameData.Money);
        UpdateBuyMode?.Invoke();
    }

    private void SubtractPrestige(double p)
    {
        gameData.TotalPrestige -= p;
        TextManager.displayValue(pTotalText, gameData.TotalPrestige, "p");
        UpdateExplanation();
    }

    public void Prestige()
    {
        if (gameData.NextPrestige >= 1)
        {
            gameData.Money = 0;
            gameData.RunMoney = 0;
            gameData.TotalPrestige += gameData.NextPrestige;
            gameData.NextPrestige = 0;
            loadData();
            PrestigeReset?.Invoke();
        }
    }
}
