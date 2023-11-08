using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;

public class BallManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public Ball ball;
    public static event Action<GameObject, Ball> BallCollected;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoneyManager.PrestigeReset += ballReset;
        GameData.loadGame += ballReset;
    }

    private void Update()
    {
        if (rb.velocity.y == 0f)
        {
            rb.AddRelativeForce(new Vector2(0.5f, 1f) * 3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(ball.isEnabled)
                BallCollected?.Invoke(collision.gameObject, ball);
            ballReset();
        }
    }

    public void ballReset()
    {
        MoneyManager.PrestigeReset -= ballReset;
        GameData.loadGame -= ballReset;
        Destroy(this.gameObject);
    }
}
