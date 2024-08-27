using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : BallManager
{
    [SerializeField]
    private float bounceAddition = 5f;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        money += bounceAddition;
    }
}
