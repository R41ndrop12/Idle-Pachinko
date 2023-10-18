using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball", menuName = "Ball")]
public class Ball : ScriptableObject
{
    //BaseRate
    [SerializeField]
    [Range(0.05f, 10)]
    //Speed at which ball cooldown resets
    private float baseRate;
    //Steep reduction of the cooldown per upgrade
    public float rateReduction;
    private int baseRateUpgrades = 0;
    [SerializeField]
    //Cost of next upgrade
    private float baseRateCost = 0;
    //Increases price of upgrades by n x multiplier
    public float rateCostMultiplier = 1.15f;
    [SerializeField]
    //Base threshold limit before changes
    private float baseRatePurchaseThreshhold = 10;
    //Offsets threshold
    public float rateThreshholdOffset = 5f;
    //Decreases cooldown by n x multiplier
    public float ratePurchaseMultiplier = 0.9f;
    //Increases threshold by n x multiplier, ie 10, 15, 22.5, etc.
    public float rateThresholdMultiplier = 1.5f;

    //BaseAmount
    [SerializeField]
    //Base amount cost
    private float baseAmountDroppedCost = 50f;
    public float amountDroppedMultiplier = 3.5f;
    
    [SerializeField]
    private float baseAutoDropCost = 1000f;
    [SerializeField]
    private bool baseSpawnBall = false;

    public int money = 1;

    public GameObject ball;
    public Color ballColor;

    //PublicRate
    [HideInInspector]
    public float rate;
    [HideInInspector]
    public int rateUpgrades = 0;
    [HideInInspector]
    public float rateCost = 0;
    [HideInInspector]
    public float ratePurchaseThreshhold = 10;
    [HideInInspector]
    public int amountDropped;
    [HideInInspector]
    public float amountDroppedCost;
    [HideInInspector]
    public bool autoDrop = false;
    [HideInInspector]
    public bool spawnBall = false;
    [HideInInspector]
    public float autoDropCost = 0f;
    private void OnEnable()
    {
        rate = baseRate;
        rateUpgrades = baseRateUpgrades;
        rateCost = baseRateCost;
        amountDropped = 1;
        autoDrop = false;
        spawnBall = baseSpawnBall;
        amountDroppedCost = baseAmountDroppedCost;
        ratePurchaseThreshhold = baseRatePurchaseThreshhold;
        autoDropCost = baseAutoDropCost;
    }

    public void enableBall()
    {
        spawnBall = true;
    }



}