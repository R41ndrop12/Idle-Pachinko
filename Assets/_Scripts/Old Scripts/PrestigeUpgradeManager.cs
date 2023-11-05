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
    public static event Action UpdateCooldownMax;
    public static event Action UpdateMultPercentage;
    public static event Action UpdateMult;
    private Data gameData;
    
    [Header("Cooldown Upgrades")]
    public TextMeshProUGUI cooldownCostTxt;
    public TextMeshProUGUI cooldownMaxTxt;
    private float cooldownUpgradeCost = 2;

    [Header("Multiplier Percentage Upgrades")]
    public TextMeshProUGUI multPercentCostText;
    public TextMeshProUGUI multPercentText;
    private float multPercentageUpgradeCost = 1;

    [Header("Multiplier Upgrades")]
    public TextMeshProUGUI multCostText;
    public TextMeshProUGUI multText;
    private float multUpgradeCost = 16;
    // Start is called before the first frame update
    void Awake()
    {
        gameData = FindObjectOfType<GameData>()._data;
        GameData.loadGame += loadData;
        loadData();
    }

    public void UpgradeCooldownMax()
    {
        if (gameData.TotalPrestige >= cooldownUpgradeCost && gameData.PrestigeUpgradeCount[0] < 5)
        {
            SubtractPrestige?.Invoke(Mathf.Round(cooldownUpgradeCost));
            gameData.PrestigeUpgradeCount[0]++;
            UpdateCooldownMax?.Invoke();
            cooldownUpgradeCost = Mathf.Round(2 * Mathf.Pow(2f, gameData.PrestigeUpgradeCount[0]));
            if(gameData.PrestigeUpgradeCount[0] < 5)
                TextManager.displayValue(cooldownCostTxt, cooldownUpgradeCost, "p");
            else
                TextManager.displayValue(cooldownCostTxt, "MAX");
            TextManager.displayValue(cooldownMaxTxt, gameData.PrestigeUpgradeCount[0], " / 5");
        }
    }

    public void UpgradeMultiplierPercentage()
    {
        if (gameData.TotalPrestige >= multPercentageUpgradeCost)
        {
            SubtractPrestige?.Invoke(Mathf.Round(multPercentageUpgradeCost));
            gameData.PrestigeUpgradeCount[1]++;
            UpdateMultPercentage?.Invoke();
            multPercentageUpgradeCost = Mathf.Round(Mathf.Pow(1.5f, gameData.PrestigeUpgradeCount[1] + 1f));
            TextManager.displayValue(multPercentCostText, multPercentageUpgradeCost, "p");
            TextManager.displayValue(multPercentText, "+", gameData.PrestigeUpgradeCount[1]*10, "%");
        }
    }

    public void UpgradeMultiplier()
    {
        if (gameData.TotalPrestige >= multUpgradeCost)
        {
            SubtractPrestige?.Invoke(Mathf.Round(multUpgradeCost));
            gameData.PrestigeUpgradeCount[2]++;
            UpdateMultPercentage?.Invoke();
            multUpgradeCost = Mathf.Round(16 * Mathf.Pow(2, gameData.PrestigeUpgradeCount[2]));
            TextManager.displayValue(multCostText, multUpgradeCost, "p");
            TextManager.displayValue(multText, "x", Mathf.Pow(2, gameData.PrestigeUpgradeCount[2]));
        }
    }

    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        UpdateMultPercentage?.Invoke();
        multUpgradeCost = Mathf.Round(16 * Mathf.Pow(2, gameData.PrestigeUpgradeCount[2]));
        TextManager.displayValue(multCostText, multUpgradeCost, "p");
        TextManager.displayValue(multText, "x", Mathf.Pow(2, gameData.PrestigeUpgradeCount[2]));

        UpdateMultPercentage?.Invoke();
        multPercentageUpgradeCost = Mathf.Round(gameData.PrestigeUpgradeCount[1] * 2);
        TextManager.displayValue(multPercentCostText, multPercentageUpgradeCost, "p");
        TextManager.displayValue(multPercentText, "+", gameData.PrestigeUpgradeCount[1] * 10, "%");

        UpdateCooldownMax?.Invoke();
        cooldownUpgradeCost = Mathf.Round(2 * Mathf.Pow(2f, gameData.PrestigeUpgradeCount[0]));
        if (gameData.PrestigeUpgradeCount[0] < 5)
            TextManager.displayValue(cooldownCostTxt, cooldownUpgradeCost, "p");
        else
            TextManager.displayValue(cooldownCostTxt, "MAX");
        TextManager.displayValue(cooldownMaxTxt, gameData.PrestigeUpgradeCount[0], " / 5");
    }
}
