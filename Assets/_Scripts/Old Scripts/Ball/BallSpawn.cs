using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallSpawn : MonoBehaviour
{
    private Data gameData;
    [Header("Spawn Settings")]
    [SerializeField]
    private Vector2 speedRange = new(2f, 10f);
    [SerializeField]
    private float directionRange = 0.5f;
    [SerializeField]
    private float spawnTime = 1f;
    [Header("Ball Amount")]
    [SerializeField]
    private int[] spawnedBalls;
    [SerializeField]
    private int currentBall;
    
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>()._data;
        BallManager.BallDestroyed += collectBall;
        BallTypeChange.changeBallType += changeCurrentBall;
        StartCoroutine(AutoSpawnBall());
    }

    public void changeCurrentBall(int b)
    {
        currentBall = b;
    }

    public void collectBall(int b)
    {
        spawnedBalls[b]--;
    }
    public void SpawnBallDynamic()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        gameData.balls[currentBall].spawnPoint.position = mousePosition;
        SpawnBall();
    }

    IEnumerator AutoSpawnBall()
    {
        SpawnBall();
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(AutoSpawnBall());
    }

    void SpawnBall()
    {
        for (int i = 0; i < spawnedBalls.Length; i++)
        { 
            Transform currentSpawn = gameData.balls[i].spawnPoint;
            if (spawnedBalls[i] < gameData.balls[i].spawnLimit)
            {
                GameObject ball = Instantiate(gameData.balls[i].ball, currentSpawn.position, Quaternion.identity);
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                float speed = Random.Range(speedRange.x, speedRange.y);
                float directionModifier = Random.Range(-directionRange, directionRange);
                while (directionModifier >= -0.2f && directionModifier <= 0.2f)
                {
                    directionModifier = Random.Range(-directionRange, directionRange);
                }
                rb.AddRelativeForce(new Vector2(directionModifier, 1f) * speed);
                ball.GetComponent<BallManager>().b = i;
                spawnedBalls[i]++;
            }
        }
    }

}
