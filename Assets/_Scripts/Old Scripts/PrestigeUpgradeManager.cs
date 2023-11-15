using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using Utility;

public class PrestigeUpgradeManager : MonoBehaviour
{

    public static event Action<double> SubtractPrestige;
    public static event Action UpdateCooldown;
    public static event Action UpdateIncomeBoost;
    private Data gameData;
    
    [Header("Cooldown Upgrade")]
    public TextMeshProUGUI cooldownCostTxt;
    public TextMeshProUGUI cooldownTxt;
    private double cooldownUpgradeCost = 2;

    [Header("Income Boost Upgrade")]
    public TextMeshProUGUI incomeBoostCostTxt;
    public TextMeshProUGUI incomeBoostTxt;
    private double incomeBoostCost = 2;

    [Header("Plink Upgrade")]
    public TextMeshProUGUI plinkCostTxt;
    public TextMeshProUGUI plinkTxt;
    private double plinkCost = 2;
    public GameObject[] plink;
    // Start is called before the first frame update
    void Awake()
    {
        gameData = FindObjectOfType<GameData>()._data;
        GameData.loadGame += loadData;
        loadData();
    }

    public void UpgradeCooldown()
    {
        if (gameData.TotalPrestige >= cooldownUpgradeCost && gameData.PrestigeUpgradeCount[0] < 10)
        {
            SubtractPrestige?.Invoke(Math.Round(cooldownUpgradeCost));
            gameData.PrestigeUpgradeCount[0]++;
            Cooldown();
        }
    }

    public void UpgradeIncomeBoost()
    {
        if (gameData.TotalPrestige >= incomeBoostCost)
        {
            SubtractPrestige?.Invoke(Math.Round(incomeBoostCost));
            gameData.PrestigeUpgradeCount[1]++;
            IncomeBoost();
        }
    }

    public void UpgradePlink()
    {
        if (gameData.TotalPrestige >= plinkCost)
        {
            SubtractPrestige?.Invoke(Math.Round(plinkCost));
            gameData.PrestigeUpgradeCount[2]++;
            Plink();
        }
    }

    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        Cooldown();
        IncomeBoost();
        Plink();
    }

    void Cooldown()
    {
        UpdateCooldown?.Invoke();
        cooldownUpgradeCost = 5 * Math.Round(Math.Pow(4f, 2.5f * gameData.PrestigeUpgradeCount[0]));
        TextManager.displayValue(cooldownTxt, "-", gameData.PrestigeUpgradeCount[0] * 10, "%");
        if (gameData.PrestigeUpgradeCount[0] < 10)
            TextManager.displayValue(cooldownCostTxt, cooldownUpgradeCost, "p");
        else
            TextManager.displayValue(cooldownCostTxt, "MAX");
    }

    void IncomeBoost()
    {
        UpdateIncomeBoost?.Invoke();
        incomeBoostCost = 2 * Math.Round(Math.Pow(1.5f, gameData.PrestigeUpgradeCount[1]));
        TextManager.displayValue(incomeBoostCostTxt, incomeBoostCost, "p");
        TextManager.displayValue(incomeBoostTxt, "+", 10 + (gameData.PrestigeUpgradeCount[1] * 10 + ((gameData.PrestigeUpgradeCount[1]) * (gameData.PrestigeUpgradeCount[1] + 1) / 2)), "%");
    }

    void Plink()
    {
        plinkCost = 10000 * Math.Pow(10, Math.Pow(3, gameData.PrestigeUpgradeCount[2]));
        if (gameData.PrestigeUpgradeCount[2] < 3)
            TextManager.displayValue(plinkCostTxt, plinkCost, "p");
        else
            TextManager.displayValue(plinkCostTxt, "MAX");
        TextManager.displayValue(plinkTxt, gameData.PrestigeUpgradeCount[2]);
        for(int i = 0; i < gameData.PrestigeUpgradeCount[2]; i++)
        {
            plink[i].SetActive(true);
        }

    }
}
