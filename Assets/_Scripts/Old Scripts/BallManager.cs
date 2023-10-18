using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private ProbabilityManager pm;
    private MoneyManager mm;
    private Rigidbody2D rb;
    public float money = 1f;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<ProbabilityManager>();
        mm = FindObjectOfType<MoneyManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.y >= 0 && rb.velocity.y <= 0.1f)
        {
            rb.AddRelativeForce(new Vector2(0.5f, 1f) * 3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            MultiplierManager multM = collision.gameObject.GetComponent<MultiplierManager>();
            pm.AddProbability(multM.posFromCenter);
            mm.AddMoney(money * multM.multiplier);
            Destroy(this.gameObject);
        }
    }
}
