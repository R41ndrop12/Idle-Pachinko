using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using Utility;

public class PrestigeUpgradeManager : MonoBehaviour
{

    public static event Action<float> SubtractPrestige;
    public static event Action UpdateCooldown;
    public static event Action UpdateIncomeBoost;
    private Data gameData;
    
    [Header("Cooldown Upgrade")]
    public TextMeshProUGUI cooldownCostTxt;
    public TextMeshProUGUI cooldownTxt;
    private float cooldownUpgradeCost = 2;

    [Header("Income Boost Upgrade")]
    public TextMeshProUGUI incomeBoostCostTxt;
    public TextMeshProUGUI incomeBoostTxt;
    private float incomeBoostCost = 2;
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
            SubtractPrestige?.Invoke(Mathf.Round(cooldownUpgradeCost));
            gameData.PrestigeUpgradeCount[0]++;
            Cooldown();
        }
    }

    public void UpgradeIncomeBoost()
    {
        if (gameData.TotalPrestige >= incomeBoostCost)
        {
            SubtractPrestige?.Invoke(Mathf.Round(incomeBoostCost));
            gameData.PrestigeUpgradeCount[1]++;
            IncomeBoost();
        }
    }

    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        Cooldown();
        IncomeBoost();
    }

    void Cooldown()
    {
        UpdateCooldown?.Invoke();
        cooldownUpgradeCost = 5 * Mathf.Round(Mathf.Pow(4f, 2.5f * gameData.PrestigeUpgradeCount[0]));
        TextManager.displayValue(cooldownTxt, "-", gameData.PrestigeUpgradeCount[0] * 10, "%");
        if (gameData.PrestigeUpgradeCount[0] < 10)
            TextManager.displayValue(cooldownCostTxt, cooldownUpgradeCost, "p");
        else
            TextManager.displayValue(cooldownCostTxt, "MAX");
    }

    void IncomeBoost()
    {
        UpdateIncomeBoost?.Invoke();
        incomeBoostCost = 2 * Mathf.Round(Mathf.Pow(1.75f, gameData.PrestigeUpgradeCount[1]));
        TextManager.displayValue(incomeBoostCostTxt, incomeBoostCost, "p");
        TextManager.displayValue(incomeBoostTxt, "+", gameData.PrestigeUpgradeCount[1] * 10, "%");
    }
}
