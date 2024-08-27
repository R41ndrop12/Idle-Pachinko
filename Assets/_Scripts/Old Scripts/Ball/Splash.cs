using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : BallManager
{
    public static event Action<Vector3, float, float> SplashCollision;
    [SerializeField]
    private float splashRadius = 3f;
    [SerializeField]
    private float splashMultiplier = 1.05f;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        SplashCollision?.Invoke(transform.position, splashRadius, splashMultiplier);
    }
}
