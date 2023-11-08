using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallSpawn : MonoBehaviour
{
    public Data gameData;
    public Transform spawnPoint;
    public Vector2 speedRange = new(2f, 10f);
    public float directionRange = 0.5f;
    public static event Action LoadBall;
    public bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        UpgradeManager.SpawnBall += spawnBall;
        gameData = FindObjectOfType<GameData>()._data;
        GameData.loadGame += resetCoroutine;
        MoneyManager.PrestigeReset += resetCoroutine;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBalls();
        }
    }

    private void resetCoroutine()
    {
        StopAllCoroutines();
        LoadBall?.Invoke();
    }

    public void SpawnBalls()
    {
        List<Ball> balls = gameData.BallDataList;
        if (isRunning)
        {
            isRunning = false;
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].isEnabled)
                {
                    if (balls[i].spawnBall && !balls[i].autoDrop)
                    {
                        StartCoroutine(SpawnBall(balls[i]));
                    }
                }
            }
            isRunning = true;
        }
    }

    public void spawnBall(Ball b)
    {
        StartCoroutine(SpawnBall(b));
    }

    IEnumerator SpawnBall(Ball b)
    {
        b.spawnBall = false;
        for (int j = 0; j < b.amountDropped; j++)
        {
            GameObject ball = Instantiate(b.ball, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            float speed = Random.Range(speedRange.x, speedRange.y);
            float directionModifier = Random.Range(-directionRange, directionRange);
            while (directionModifier >= -0.2f && directionModifier <= 0.2f)
            {
                directionModifier = Random.Range(-directionRange, directionRange);
            }
            rb.AddRelativeForce(new Vector2(directionModifier, 1f) * speed);
            ball.GetComponent<BallManager>().ball = b;
            ball.GetComponent<SpriteRenderer>().color = (b.ballColor);
        }
        float cooldown = b.cooldown * (1 - (gameData.PrestigeUpgradeCount[0] / 10f));
        if (cooldown <= 0.2f && b.autoDrop)
        {
            b.progressBar.fillAmount = 1;
            yield return new WaitForSeconds(cooldown);
        }
        else
        {
            for (float k = 0; k < cooldown; k += 0.01f)
            {
                b.progressBar.fillAmount = k / cooldown;
                yield return new WaitForSeconds(0.01f);
            }
        }
        b.spawnBall = true;
        if (b.autoDrop)
            spawnBall(b);
    }
}
