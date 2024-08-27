using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dio : BallManager
{
    [SerializeField]
    private float timeMultiplier = 1.05f;
    [SerializeField]
    private float timeInterval = 0.5f;
    private float startTime;
    private double startMoney;
    public override void Start()
    {
        base.Start();
        startMoney = money;
        startTime = Time.time;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        float intervals = (Time.time - startTime) / timeInterval;
        money = startMoney * Mathf.Pow(timeMultiplier, intervals);
        base.OnCollisionEnter2D(collision);
    }
}
