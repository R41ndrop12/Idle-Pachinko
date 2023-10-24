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
    public Ball ball;
    private GameData gameData;
    public Button spawnButton;
    public SpriteRenderer ballColor;
    public Image fill;
    public TextMeshProUGUI cooldownCostText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI cooldownCountText;
    public TextMeshProUGUI multCostText;
    public TextMeshProUGUI multText;
    public TextMeshProUGUI multCountText;
    public TextMeshProUGUI ballDollarAmountText;
    public GameObject nextBall;
    public static event Action<float> SubtractMoney;
    public static event Action<Ball> SpawnBall;
    // Start is called before the first frame update
    void Awake()
    {
        gameData = FindObjectOfType<GameData>();
        ball.progressBar = fill;
        ballColor.color = ball.ballColor;
        fill.color = ball.ballColor;
        spawnButton.onClick.AddListener(() => SpawnBall?.Invoke(ball));
        resetCooldownPrice();
        resetMultiplierPrice();
        MoneyManager.PrestigeReset += resetCooldownPrice;
        MoneyManager.PrestigeReset += resetMultiplierPrice;
    }

    private void OnDisable()
    {
        MoneyManager.PrestigeReset -= resetCooldownPrice;
        MoneyManager.PrestigeReset -= resetMultiplierPrice;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ball.spawnBall)
            {
                SpawnBall?.Invoke(ball);
            }
        }
    }

    public void resetCooldownPrice()
    {
        ball.cooldown = ball.baseCooldown * (Mathf.Pow(0.95f, ball.cooldownUpgrades) - (ball.cooldownUpgrades / 90.125f));
        ball.cooldownCost = ball.baseCooldownCost * Mathf.Pow(ball.cooldownCostMultiplier, ball.cooldownUpgrades);
        if (ball.cooldownUpgrades < 5)
        {
            if (nextBall != null)
                nextBall.SetActive(false);
            TextManager.displayValue(cooldownCountText, ball.cooldownUpgrades + " / 5");
        }
        else if (ball.cooldownUpgrades < 10)
        {
            if (nextBall != null)
                nextBall.SetActive(true);
            TextManager.displayValue(cooldownCountText, ball.cooldownUpgrades + " / 10");
        }
        else
        {
            if (!ball.autoDrop)
            {
                ball.autoDrop = true;
                if (ball.spawnBall)
                {
                    SpawnBall?.Invoke(ball);
                }
            }
            TextManager.displayValue(cooldownCountText, ball.cooldownUpgrades + " / 25");

        }
        TextManager.displayValue(ballDollarAmountText, "$", ball.money);
        TextManager.displayValue(cooldownCostText, "$", ball.cooldownCost);
        TextManager.displayValue(cooldownText, ball.cooldown, "s");
    }
    public void CooldownPrice()
    {
        if (ball.cooldown > ball.minCooldown && gameData.Money >= ball.cooldownCost)
        {
            SubtractMoney?.Invoke(ball.cooldownCost);
            ball.cooldownUpgrades++;
            if (ball.cooldownUpgrades < 5)
            {
                if(nextBall != null)
                    nextBall.SetActive(false);
                TextManager.displayValue(cooldownCountText, ball.cooldownUpgrades + " / 5");
            }
            else if (ball.cooldownUpgrades < 10)
            {
                if (nextBall != null)
                    nextBall.SetActive(true);
                TextManager.displayValue(cooldownCountText, ball.cooldownUpgrades + " / 10");
            }
            else
            {
                if (!ball.autoDrop)
                {
                    ball.autoDrop = true;
                    if (ball.spawnBall)
                    {
                        SpawnBall?.Invoke(ball);
                    }
                }
                TextManager.displayValue(cooldownCountText, ball.cooldownUpgrades + " / 25");

            }
            ball.cooldown = ball.baseCooldown * (Mathf.Pow(0.95f, ball.cooldownUpgrades) - (ball.cooldownUpgrades/90.125f));
            ball.cooldownCost = ball.baseCooldownCost * Mathf.Pow(ball.cooldownCostMultiplier, ball.cooldownUpgrades);
            if (ball.cooldown < ball.minCooldown)
            {
                ball.cooldown = ball.minCooldown;
                TextManager.displayValue(cooldownCostText, "$-");
                TextManager.displayValue(cooldownText, ball.cooldown, "s");
            }
            else
            {
                ball.cooldownCost = ball.cooldownCost * ball.cooldownCostMultiplier;
                TextManager.displayValue(cooldownCostText, "$", ball.cooldownCost);
                TextManager.displayValue(cooldownText, ball.cooldown, "s");
            }
        }
    }

    public void resetMultiplierPrice()
    {
        ball.multiplierCost = ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, ball.multUpgrades); ;
        ball.multiplier = 1 + 0.1f * ball.multUpgrades;
        TextManager.displayValue(multCostText, "$", ball.multiplierCost);
        TextManager.displayValue(multText, "x", ball.multiplier);
        TextManager.displayValue(ballDollarAmountText, "$", ball.multiplier * ball.money);
        TextManager.displayValue(multCountText, ball.multUpgrades);
    }
    public void MultiplierPrice()
    {
        if (gameData.Money >= ball.multiplierCost)
        {
            SubtractMoney?.Invoke(ball.multiplierCost);
            ball.multUpgrades++;
            ball.multiplierCost = ball.baseMultiplierCost * Mathf.Pow(ball.multCostMultiplier, ball.multUpgrades); ;
            ball.multiplier = 1 + 0.1f * ball.multUpgrades;
            TextManager.displayValue(multCostText, "$", ball.multiplierCost);
            TextManager.displayValue(multText, "x", ball.multiplier);
            TextManager.displayValue(ballDollarAmountText, "$", ball.multiplier * ball.money);
            TextManager.displayValue(multCountText, ball.multUpgrades);
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
