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
    public static event Action UpdateMult;
    private Data gameData;
    
    [Header("Cooldown Upgrade")]
    public TextMeshProUGUI cooldownCostTxt;
    public TextMeshProUGUI cooldownTxt;
    private float cooldownUpgradeCost = 2;
    // Start is called before the first frame update
    void Awake()
    {
        gameData = FindObjectOfType<GameData>()._data;
        GameData.loadGame += loadData;
        loadData();
    }

    public void UpgradeCooldown()
    {
        if (gameData.TotalPrestige >= cooldownUpgradeCost && gameData.PrestigeUpgradeCount[0] < 9)
        {
            SubtractPrestige?.Invoke(Mathf.Round(cooldownUpgradeCost));
            gameData.PrestigeUpgradeCount[0]++;
            Cooldown();
        }
    }

    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        Cooldown();
    }

    void Cooldown()
    {
        UpdateCooldown?.Invoke();
        cooldownUpgradeCost = Mathf.Round(Mathf.Pow(2f, 1.1f * (gameData.PrestigeUpgradeCount[0] + 1f)));
        TextManager.displayValue(cooldownCostTxt, cooldownUpgradeCost, "p");
        TextManager.displayValue(cooldownTxt, "-", gameData.PrestigeUpgradeCount[0] * 10, "%");
    }
}
