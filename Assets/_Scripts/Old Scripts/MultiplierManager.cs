using System;
using UnityEngine;

public class MultiplierManager : MonoBehaviour
{
    public float multiplier = 0f;
    public int posFromCenter = 0;
    public static event Action<double> AddMoney;
    public static event Action<int> AddProbability;
    public Color color;

    private void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        BallManager.BallCollected += OnBallCollision;
    }

    void OnBallCollision(GameObject gm, Ball ball)
    {
        if(gm == this.gameObject)
        {
            AddMoney?.Invoke(ball.money * multiplier * ball.multiplier);
            AddProbability?.Invoke(posFromCenter);
        }
    }
}
