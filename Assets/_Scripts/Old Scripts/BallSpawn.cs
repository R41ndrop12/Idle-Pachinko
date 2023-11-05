
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
    public Vector2 speedRange = new Vector2(2f, 10f);
    public float directionRange = 0.5f;
    public static event Action<Ball> autoSpawn;
    public static event Action loadBall;
    // Start is called before the first frame update
    void Start()
    {
        UpgradeManager.AutoSpawnBall += AutoSpawnBallWrapper;
        gameData = FindObjectOfType<GameData>()._data;
        GameData.loadGame += resetCoroutine;
        MoneyManager.PrestigeReset += resetCoroutine;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBallWrapper();
        }
    }

    private void resetCoroutine()
    {
        StopAllCoroutines();
        loadBall?.Invoke();   
    }

    public void SpawnBallWrapper()
    {
        List<Ball> balls = gameData.BallDataList;
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].isEnabled)
            {
                if (balls[i].spawnBall && !balls[i].autoDrop)
                {
                    balls[i].spawnBall = false;
                    StartCoroutine(SpawnBall(balls[i]));
                }
            }
        }
    }

    void AutoSpawnBallWrapper(Ball b)
    {
        StartCoroutine(AutoSpawnBall(b));
    }
    IEnumerator SpawnBall(Ball b)
    {
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
        if (b.cooldown <= 0.2f)
        {
            b.progressBar.fillAmount = 1;
            yield return new WaitForSeconds(b.cooldown);
        }
        else
        {
            float cooldown = b.cooldown;
            for (float k = 0; k < cooldown; k += 0.02f)
            {
                b.progressBar.fillAmount = k / cooldown;
                yield return new WaitForSeconds(0.02f);
            }
            b.progressBar.fillAmount = 1;
        }
        b.spawnBall = true;
    }

    IEnumerator AutoSpawnBall(Ball b)
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
        if (b.cooldown <= 0.2f)
        {
            b.progressBar.fillAmount = 1;
            yield return new WaitForSeconds(b.cooldown);
        }
        else
        {
            float cooldown = b.cooldown;
            for (float k = 0; k < cooldown; k += 0.02f)
            {
                b.progressBar.fillAmount = k / cooldown;
                yield return new WaitForSeconds(0.02f);
            }
            b.progressBar.fillAmount = 1;
        }
        b.spawnBall = true;
        autoSpawn?.Invoke(b);
    }
}
