using System;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI multCostText;
    public TextMeshProUGUI multText;
    public TextMeshProUGUI multCountText;
    public TextMeshProUGUI ballDollarAmountText;
    public GameObject nextBall;
    public static event Action<float> SubtractMoney;
    public static event Action<Ball> AutoSpawnBall;
    // Start is called before the first frame update
    void Awake()
    {
        gameData = FindObjectOfType<GameData>()._data;
        ball = gameData.BallDataList[ballNum];
        ball.enableBall();
        ball.progressBar = fill;
        fill.color = ball.ballColor;
        loadData();
        MoneyManager.PrestigeReset += resetUpgrades;
        PrestigeUpgradeManager.UpdateMultPercentage += checkMultiplierPrice;
        BallSpawn.loadBall += loadData;
        BallSpawn.autoSpawn += autoSpawn;
    }

    private void OnDisable()
    {
        MoneyManager.PrestigeReset -= resetUpgrades;
        PrestigeUpgradeManager.UpdateMultPercentage -= checkMultiplierPrice;
        BallSpawn.loadBall -= loadData;
        BallSpawn.autoSpawn -= autoSpawn;
    }

    private void OnEnable()
    {
        MoneyManager.PrestigeReset += resetUpgrades;
        PrestigeUpgradeManager.UpdateMultPercentage += checkMultiplierPrice;
        BallSpawn.loadBall += loadData;
        BallSpawn.autoSpawn += autoSpawn;
    }
    void loadData()
    {
        gameData = FindObjectOfType<GameData>()._data;
        checkMultiplierPrice();
    }

    public void resetUpgrades()
    {

        gameData.BallMultiplierCount[ballNum] = 0;
        checkMultiplierPrice();

    }
    void autoSpawn(Ball b)
    {
        if(ball == b && ball.spawnBall && ball.autoDrop)
        {
            ball.spawnBall = false;
            AutoSpawnBall?.Invoke(ball);
        }
    }

    public void checkMultiplierPrice()
    {
        ball.money = ball.baseMoney * (Mathf.Pow(2, gameData.PrestigeUpgradeCount[2]));
        ball.multiplierCost = ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, gameData.BallMultiplierCount[ballNum]);
        ball.multiplier = 1 + gameData.BallMultiplierCount[ballNum] * (gameData.PrestigeUpgradeCount[1] + 1f)/10f;
        TextManager.displayValue(multCostText, "$", ball.multiplierCost);
        TextManager.displayValue(multText, "x", ball.multiplier);
        TextManager.displayValue(ballDollarAmountText, "$", ball.multiplier * ball.money);
        TextManager.displayValue(multCountText, gameData.BallMultiplierCount[ballNum]);
    }

    public void MultiplierPrice()
    {
        if (gameData.Money >= ball.multiplierCost)
        {
            SubtractMoney?.Invoke(ball.multiplierCost);
            gameData.BallMultiplierCount[ballNum]++;
            checkMultiplierPrice();
        }
    }

    /*public void AutoDropPrice()
    {
        if (!ball.autoDrop && mm.money >= ball.autoDropCost)
        {
            mm.SubtractMoney(ball.autoDropCost);
            ball.autoDrop = true;
            TextManager.displayValue(autoDropCostText, "$-");
            TextManager.displayValue(autoDropText, "On");
            if (ball.spawnBall)
            {
                bs.SpawnBallWrapper(ball);
            }
        }
    }*/
}
