using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class UpgradeManager : MonoBehaviour
{
    public Ball[] balls;
    private MoneyManager mm;
    private BallSpawn bs;
    public TextMeshProUGUI[] rateCostText;
    public TextMeshProUGUI[] rateCooldowntText;
    public TextMeshProUGUI[] amountCostText;
    public TextMeshProUGUI[] amountText;
    public TextMeshProUGUI[] autoDropCostText;
    public TextMeshProUGUI[] autoDropText;
    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MoneyManager>();
        bs = FindObjectOfType<BallSpawn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RatePrice(int ball)
    {
        Ball b = balls[ball];
        if (mm.money >= b.rateCost && b.rate > 0.05f)
        {
            mm.SubtractMoney(b.rateCost);
            b.rateUpgrades++;
            b.rate -= b.rateReduction;
            if (b.rateUpgrades == Mathf.Round(b.ratePurchaseThreshhold) - b.rateThreshholdOffset)
            {
                b.rate *= b.ratePurchaseMultiplier;
                b.ratePurchaseThreshhold *= b.rateThresholdMultiplier;
            }
            if(b.rate < 0.05f)
            {
                b.rate = 0.05f;
            }
            b.rateCost = b.rateCost * b.rateCostMultiplier;
            TextManager.displayValue(rateCostText[ball], "$", b.rateCost);
            TextManager.displayValue(rateCooldowntText[ball], b.rate, "s");
        }
    }

    public void AmountPrice(int ball)
    {
        Ball b = balls[ball];
        if (mm.money >= b.amountDroppedCost && b.amountDropped < 10)
        {
            mm.SubtractMoney(b.amountDroppedCost);
            b.amountDropped++;
            b.amountDroppedCost = b.amountDroppedCost * b.amountDroppedMultiplier;
            TextManager.displayValue(amountCostText[ball], "$", b.amountDroppedCost);
            TextManager.displayValue(amountText[ball], b.amountDropped);
        }
    }

    public void AutoDropPrice(int ball)
    {
        Ball b = balls[ball];
        if (!b.autoDrop && mm.money >= b.autoDropCost)
        {
            mm.SubtractMoney(b.autoDropCost);
            b.autoDrop = true;
            TextManager.displayValue(autoDropCostText[ball], "$-");
            TextManager.displayValue(autoDropText[ball], "On");
            if (b.spawnBall)
            {
                bs.SpawnBallWrapper(ball);
            }
        }
    }
}
