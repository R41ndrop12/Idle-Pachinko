using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private ProbabilityManager pm;
    private MoneyManager mm;
    public float money = 1f;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<ProbabilityManager>();
        mm = FindObjectOfType<MoneyManager>();
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
