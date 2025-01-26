using System;
using TMPro;
using UnityEngine;

public class MultiplierManager : MonoBehaviour
{
    public float multiplier = 0f;
    public int posFromCenter = 0;
    public TextMeshProUGUI multText;
    public static event Action<double> AddMoney;
    public static event Action<int> AddProbability;

    private void Start()
    {
        BallManager.BallCollected += OnBallCollision;
        multText.SetText(multiplier + "x");
    }

    void OnBallCollision(GameObject gm, double money)
    {
        if(gm == this.gameObject)
        {
            AddMoney?.Invoke(money * multiplier);
            AddProbability?.Invoke(posFromCenter);
        }
    }
}
