using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    public GameObject gm;
    public GameObject[] gms;
    private MoneyManager moneyManager;
    public Ball ball;

    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void Unlock(float price)
    {
        if(moneyManager.money >= price)
        {
            moneyManager.SubtractMoney(price);
            for(int i = 0; i < gms.Length; i++)
            {
                gms[i].SetActive(true);
            }
            if(gm != null)
                gm.SetActive(false);
            if(ball != null)
                ball.enableBall();
        }
    }
}
