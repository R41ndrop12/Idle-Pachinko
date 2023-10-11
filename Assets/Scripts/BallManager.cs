using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private ProbabilityManager pm;
    private MoneyManager mm;
    private Transform gm;
    public float money = 1f;
    private float[] multiplier = { 0.5f, 1.25f, 1.75f, 3f, 5f, 10f, 25f, 50f, 100f };
    public float offset = 4f;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<ProbabilityManager>();
        mm = FindObjectOfType<MoneyManager>();
        gm = this.gameObject.transform;
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
