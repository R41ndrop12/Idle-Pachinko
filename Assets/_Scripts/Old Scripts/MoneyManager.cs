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
        pExplanationText.SetText("Your " + TextManager.convertNum(gameData.TotalPrestige) + "p is providing a " + TextManager.convertNum(gameData.TotalPrestige * 10f) + "% boost to your income!");
    }

    private void Add(float m)
    {
        gameData.Money += m;
        gameData.RunMoney += m;
        gameData.LifetimeMoney += m;
        gameData.NextPrestige = Mathf.Round((100 * (Mathf.Sqrt(gameData.RunMoney / Mathf.Pow(10, 8)))));
        TextManager.displayValue(mText, "$", gameData.Money);
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
        UpdateBuyMode?.Invoke();
    }
    private void SubtractMoney(float m)
    {
        gameData.Money -= m;
        TextManager.displayValue(mText, "$", gameData.Money);
        UpdateBuyMode?.Invoke();
    }

    private void SubtractPrestige(float p)
    {
        gameData.TotalPrestige -= p;
        TextManager.displayValue(pTotalText, gameData.TotalPrestige, "p");
        pExplanationText.SetText("Your " + TextManager.convertNum(gameData.TotalPrestige) + " is providing a " + TextManager.convertNum(gameData.TotalPrestige * 10f) + "% boost to your income!");
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

    IEnumerator TrackMoneyOverTime()
    {
        Vector2 currentData = new Vector2(seconds, gameData.RunMoney);
        moneyOverTime.Add(currentData);
        seconds++;
        yield return new WaitForSeconds(1f);
        StartCoroutine("TrackMoneyOverTime");
    }
}
