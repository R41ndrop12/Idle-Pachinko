
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
        balls[0].spawnBall = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < balls.Length; i++)
            {
                if (balls[i].spawnBall)
                {
                    SpawnBallWrapper(i);
                }
            }
        }
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
            if (b.autoDrop && b.rate <= 0.2f)
            {
                progressBars[i].fillAmount = 1;
                yield return new WaitForSeconds(b.rate);
            }
            else
            {
                for (float k = 0; k < b.rate; k += 0.01f)
                {
                    progressBars[i].fillAmount = k / b.rate;
                    yield return new WaitForSeconds(0.01f);
                }
                progressBars[i].fillAmount = 1;
            }
            b.spawnBall = true;
            if (b.autoDrop)
            {
                StartCoroutine(SpawnBall(i));
            }
        }
    }
}
