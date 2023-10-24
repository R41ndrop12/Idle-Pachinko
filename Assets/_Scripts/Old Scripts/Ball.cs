using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ball", menuName = "Ball")]
public class Ball : ScriptableObject
{
    //BaseCooldown

    [Header("Cooldown Variables")]
    [SerializeField]
    [Range(0.05f, 10)]
    //Speed at which ball cooldown resets
    public float baseCooldown;
    private int baseCooldownUpgrades = 0;//store
    public int baseCooldownCost = 0;
    public float minCooldown = 0.05f;//store
    //Increases price of upgrades by n x multiplier
    public float cooldownCostMultiplier = 1.15f;

    [Space (20)]
    [Header("Multiplier Variables")]
    [SerializeField]
    //Base amount cost
    public float baseMultiplierCost = 50f;
    private int baseMultUpgrades = 0;
    private float baseMult = 1f;
    public float multCostMultiplier = 1.5f;

    [Space(20)]
    [Header("Other Variables")]
    public bool isEnabled = false;

    public int money = 1;

    public GameObject ball;
    public Color ballColor;
    public Image progressBar;

    //Public
    [HideInInspector]
    public float cooldown;
    [HideInInspector]
    public int cooldownUpgrades = 0;
    [HideInInspector]
    public float cooldownCost = 0;
    [HideInInspector]
    public int amountDropped;
    [HideInInspector]
    public float multiplier;
    [HideInInspector]
    public float multiplierCost;
    [HideInInspector]
    public int multUpgrades = 0;
    [HideInInspector]
    public bool autoDrop = false;
    [HideInInspector]
    public bool spawnBall = false;
    private void OnEnable()
    {
        resetBall();
        MoneyManager.PrestigeReset += resetBall;
    }

    public void enableBall()
    {
        spawnBall = true;
        isEnabled = true;
    }

    public void resetBall()
    {
        cooldownUpgrades = baseCooldownUpgrades;
        amountDropped = 1;
        autoDrop = false;
        spawnBall = isEnabled;
        multUpgrades = baseMultUpgrades;
    }



}