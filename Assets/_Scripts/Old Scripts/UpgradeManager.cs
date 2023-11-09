using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Utility;

public class UpgradeManager : MonoBehaviour
{
    public int ballNum;
    private Data gameData;
    private Ball ball;
    public Image fill;
    public Image amountFill;
    public Image ballImage;
    public TextMeshProUGUI multCostText;
    public TextMeshProUGUI multCountText;
    public TextMeshProUGUI ballDollarAmountText;
    public GameObject nextBall;
    public static event Action<float> SubtractMoney;
    public static event Action<Ball> SpawnBall;
    public static event Action<Mode> UpdateBuyMode;
    private Mode buyMode = Mode.One;
    private int amountBought = 1;
    // Start is called before the first frame update
    void Awake()
    {
        gameData = FindObjectOfType<GameData>()._data;
        ball = gameData.BallDataList[ballNum];
        ball.enableBall();
        ball.progressBar = fill;
        fill.color = ball.ballColor;
        ballImage.color = ball.ballColor;
        loadData();
        MoneyManager.PrestigeReset += resetUpgrades;
        BallSpawn.LoadBall += loadData;
        BuyMode.changeBuyMode += UpdateCost;
        MoneyManager.UpdateBuyMode += CalculateCost;
        UpdateBuyMode += UpdateCost;
        PrestigeUpgradeManager.UpdateCooldown += checkMultiplierPrice;
    }

    private void OnDisable()
    {
        MoneyManager.PrestigeReset -= resetUpgrades;
        BallSpawn.LoadBall -= loadData;
        BuyMode.changeBuyMode -= UpdateCost;
        MoneyManager.UpdateBuyMode -= CalculateCost;
        UpdateBuyMode -= UpdateCost;
        PrestigeUpgradeManager.UpdateCooldown -= checkMultiplierPrice;
        PrestigeUpgradeManager.UpdateIncomeBoost -= checkMultiplierPrice;
    }

    private void OnEnable()
    {
        ball.enableBall();
    }
    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        ball.autoDrop = false;
        ball.enableBall();
        CalculateCost();
    }

    public void resetUpgrades()
    {
        gameData.BallMultiplierCount[ballNum] = 0;
        ball.resetBall();
        CalculateCost();
    }

    public void checkMultiplierPrice()
    {
        int basicMultiplier = (gameData.BallMultiplierCount[ballNum] / 10);
        int intermediateMultiplier = (gameData.BallMultiplierCount[ballNum] / 50);
        if (basicMultiplier >= 3 && nextBall != null)
        {
            if (!nextBall.activeSelf)
            {
                nextBall.SetActive(true);
                UpdateBuyMode?.Invoke(buyMode);
            }
        }
        else
        {
            if (nextBall != null)
            {
                nextBall.SetActive(false);
                gameData.BallDataList[ballNum + 1].disableBall();
            }
        }
        if (basicMultiplier >= 5)
        {
            if (!ball.autoDrop)
            {
                ball.autoDrop = true;
                if (ball.spawnBall)
                {
                    SpawnBall?.Invoke(ball);
                }
            }
        }
        ball.multiplier = (1 + gameData.BallMultiplierCount[ballNum]/10f) * (basicMultiplier + 1) * Mathf.Pow(1.5f, intermediateMultiplier);
        ball.money = ball.baseMoney * (1 + gameData.TotalPrestige * (1 + gameData.PrestigeUpgradeCount[1]) / 10f);
        TextManager.displayValue(multCostText, "$", ball.multiplierCost);
        TextManager.displayValue(ballDollarAmountText, "$", ball.multiplier * ball.money, " / " + TextManager.convertNum(ball.baseCooldown * (1f - (gameData.PrestigeUpgradeCount[0]/10f))) + "s");
        TextManager.displayValue(multCountText, gameData.BallMultiplierCount[ballNum], " / " + ((basicMultiplier + 1) * 10));
        amountFill.fillAmount = (gameData.BallMultiplierCount[ballNum] - basicMultiplier * 10f) / 10f;
    }

    public void MultiplierPrice()
    {
        if (gameData.Money >= ball.multiplierCost)
        {
            gameData.BallMultiplierCount[ballNum] += amountBought;
            SubtractMoney?.Invoke(ball.multiplierCost);
            checkMultiplierPrice();
        }
    }
    public void UpdateCost(Mode mode)
    {
        buyMode = mode;
        CalculateCost();
    }
    public void CalculateCost()
    {
        float totalCost;
        switch (buyMode)
        {
            case Mode.Ten:
                amountBought = 10;
                break;
            case Mode.Hundred:
                amountBought = 100;
                break;
            case Mode.Max:
                amountBought = 1;
                totalCost = Mathf.Round(ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, gameData.BallMultiplierCount[ballNum]) * 10f) / 10f;
                while (totalCost < gameData.Money)
                {
                    totalCost += Mathf.Round(ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, gameData.BallMultiplierCount[ballNum] + amountBought) * 10f) / 10f;
                    amountBought++;
                }
                amountBought--;
                break;
            case Mode.Next:
                amountBought = 10 - (gameData.BallMultiplierCount[ballNum] - gameData.BallMultiplierCount[ballNum] / 10 * 10);
                break;
            default:
                amountBought = 1;
                break;
        }
        totalCost = Mathf.Round(ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, gameData.BallMultiplierCount[ballNum]) * 10f) / 10f;
        for (int i = 1; i < amountBought; i++)
        {
            totalCost += Mathf.Round(ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, gameData.BallMultiplierCount[ballNum] + i) * 10f) / 10f;
        }
        ball.multiplierCost = totalCost;
        checkMultiplierPrice();
    }
}
