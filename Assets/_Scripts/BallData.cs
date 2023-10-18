using System;
using UnityEngine;

[CreateAssetMenu]
public class BallData : ScriptableObject
{
    public double BallIncome(int rateCount, float multiplier) => BallBaseIncome * multiplier;
    //A way to set the base price of purchasing item and the upgrade cost in one method
    public double BallCooldownUpgradePrice(int rateCount)
        => rateCount switch
        {
            0 => BallCooldownStartCost,
            _ => BallCooldownStartCost * Math.Pow(BallCooldownCostMultiFactor, (rateCount + 1))
        };

    //Those parameters are editable though the inspector. Originally i have planed to load this data from a file (for example xls)
    [field: SerializeField]
    public double BallBaseIncome { get; set; } = 1;

    [field: SerializeField]
    public double BallCooldown { get; private set; } = 3;

    [field: SerializeField]
    public double BallCooldownStartCost { get; private set; } = 3;
    [field: SerializeField]
    public double BallCooldownCostMultiFactor { get; private set; } = 1.15;

    //We set the max count as a limit before we increase the bonus multplier for the cooldown
    public int MaxCount(float bonusCooldownMultiplier, int maxCountHelper) =>
        bonusCooldownMultiplier switch
        {
            1f => 5,
            0.95f => 10,
            _ => maxCountHelper
        };

    //Parameter used to caluclate if we should increase the bonus multiplier
    public float BonusMaxCountThreshold => 0.5f;
    //Parameter used to caluclate if we should increase the bonus multiplier
    public int MaxCountIncrement => 50;

    [field: SerializeField]
    public float Delay { get; set; } = 3f;
    [field: SerializeField]
    public float ManagerPrice { get; set; } = 1000;
    [field: SerializeField]
    public GameObject Ball{ get; set; }
    [field: SerializeField]
    public Sprite BallImage { get; set; }
}
