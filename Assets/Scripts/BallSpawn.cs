using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawn : MonoBehaviour
{
    
    public Ball[] balls;
    public Transform spawnPoint;
    public Vector2 speedRange = new Vector2(2f, 10f);
    public float directionRange = 0.5f;
    public Image[] progressBars;
    // Start is called before the first frame update
    void Start()
    {
        balls = FindObjectOfType<UpgradeManager>().balls;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBallWrapper(int i)
    {
        StartCoroutine(SpawnBall(i));
    }

    IEnumerator SpawnBall(int i)
    {
        Ball b = balls[i];
        if (b.spawnBall)
        {
            b.spawnBall = false;
            for (int j = 0; j < b.amountDropped; j++)
            {
                GameObject ball = Instantiate(b.ball, spawnPoint.position, Quaternion.identity);
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                float speed = Random.Range(speedRange.x, speedRange.y);
                float directionModifier = Random.Range(-directionRange, directionRange);
                while (directionModifier >= -0.2f && directionModifier <= 0.2f)
                {
                    directionModifier = Random.Range(-directionRange, directionRange);
                }
                rb.AddRelativeForce(new Vector2(directionModifier, 1f) * speed);
                ball.GetComponent<BallManager>().money = b.money;
                ball.GetComponent<SpriteRenderer>().color = (b.ballColor);
                Gradient gradient = new Gradient();
                gradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(b.ballColor, 0.0f), new GradientColorKey(b.ballColor, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(5, 0.0f), new GradientAlphaKey(0, 1.0f) }
                );
                ball.GetComponent<TrailRenderer>().colorGradient = gradient;
            }
            for (float k = 0; k < b.rate; k += 0.01f)
            {
                progressBars[i].fillAmount = k / b.rate;
                yield return new WaitForSeconds(0.01f);
            }
            b.spawnBall = true;
            if (b.autoDrop)
            {
                StartCoroutine(SpawnBall(i));
            }
        }
    }
}
