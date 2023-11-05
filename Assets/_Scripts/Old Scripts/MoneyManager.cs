using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using Utility;

public class MoneyManager : MonoBehaviour
{
    public static event Action PrestigeReset;
    private Data gameData;
    public TextMeshProUGUI mText;
    public TextMeshProUGUI pText;
    public TextMeshProUGUI pTotalText;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>()._data;
        MultiplierManager.AddMoney += Add;
        UpgradeManager.SubtractMoney += SubtractMoney;
        PrestigeUpgradeManager.SubtractPrestige += SubtractPrestige;
        GameData.loadGame += loadData;
        loadData();
    }

    // Update is called once per frame
    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        TextManager.displayValue(mText, "$", gameData.Money);
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
        TextManager.displayValue(pTotalText, gameData.TotalPrestige, "p");
    }

    private void Add(float m)
    {
        gameData.Money += m;
        gameData.RunMoney += m;
        gameData.LifetimeMoney += m;
        gameData.NextPrestige = Mathf.Round((45 * (Mathf.Sqrt(gameData.RunMoney / Mathf.Pow(10, 6)))));
        TextManager.displayValue(mText, "$", gameData.Money);
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
    }
    private void SubtractMoney(float m)
    {
        gameData.Money -= m;
        TextManager.displayValue(mText, "$", gameData.Money);
    }

    private void SubtractPrestige(float p)
    {
        gameData.TotalPrestige -= p;
        TextManager.displayValue(pTotalText, gameData.TotalPrestige, "p");
    }

    public void Prestige()
    {
        if (gameData.NextPrestige >= 1)
        {
            gameData.Money = 0;
            gameData.RunMoney = 0;
            gameData.TotalPrestige += gameData.NextPrestige;
            gameData.NextPrestige = 0;
            TextManager.displayValue(mText, "$", gameData.Money);
            TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
            TextManager.displayValue(pTotalText, gameData.TotalPrestige, "p");
            PrestigeReset?.Invoke();
        }
    }
}
