using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

public abstract class BallManager : MonoBehaviour
{
    private Data gameData;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [HideInInspector]
    public int b;
    private BallData ballData;
    public double money = 0;
    public static event Action<GameObject, double> BallCollected;
    public static event Action<int> BallDestroyed;
    // Start is called before the first frame update
    public virtual void Start()
    {
        gameData = FindObjectOfType<GameData>()._data;
        ballData = gameData.balls[b];
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        money = ballData.money;
        GameData.loadGame += ballReset;
        Splash.SplashCollision += splashCalculation;
    }

    public virtual void Update()
    {
        if (rb.velocity.y == 0f)
        {
            rb.AddRelativeForce(new Vector2(0.5f * Mathf.Pow(-1f, Random.Range(0,2)), 1f) * 3);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {

            BallCollected?.Invoke(collision.gameObject, money);
            BallDestroyed?.Invoke(b);
            ballReset();
        }
    }

    public virtual void splashCalculation(Vector3 splashPoint, float splashRadius, float splashMult)
    {
        float dist = Vector3.Distance(splashPoint, transform.position);
        if (dist < splashRadius && dist != 0)
            money *= splashMult;
    }

    public virtual void ballReset()
    {
        GameData.loadGame -= ballReset;
        Splash.SplashCollision -= splashCalculation;
        Destroy(this.gameObject);
    }
}
