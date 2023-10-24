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
    public static event Action SubtractMoney;
    private GameData gameData;
    public TextMeshProUGUI mText;
    public TextMeshProUGUI pText;
    public TextMeshProUGUI pTotalText;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        MultiplierManager.AddMoney += Add;
        UpgradeManager.SubtractMoney += Subtract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(float m)
    {
        gameData.Money += m;
        gameData.RunMoney += m;
        gameData.LifetimeMoney += m;
        gameData.NextPrestige = (int) (45 * (Mathf.Sqrt(gameData.RunMoney / Mathf.Pow(10, 6))));
        TextManager.displayValue(mText, "$", gameData.Money);
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
    }
    public void Subtract(float m)
    {
        gameData.Money -= m;
        TextManager.displayValue(mText, "$", gameData.Money);
    }

    public void Prestige()
    {
        gameData.Money = 0;
        gameData.RunMoney = 0;
        gameData.TotalPrestige += gameData.NextPrestige;
        gameData.NextPrestige = 0;
        TextManager.displayValue(pText, "+", gameData.NextPrestige, "p");
        TextManager.displayValue(pTotalText, gameData.NextPrestige, "p");
        PrestigeReset?.Invoke();
    }
}
