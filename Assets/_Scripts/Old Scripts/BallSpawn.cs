
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public Vector2 speedRange = new Vector2(2f, 10f);
    public float directionRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        UpgradeManager.SpawnBall += SpawnBallWrapper;
    }


    public void SpawnBallWrapper(Ball b)
    {
        StartCoroutine(SpawnBall(b));
    }

    IEnumerator SpawnBall(Ball b)
    {
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
                ball.GetComponent<BallManager>().ball = b;
                ball.GetComponent<SpriteRenderer>().color = (b.ballColor);
            }
            if (b.autoDrop && b.cooldown <= 0.2f)
            {
                b.progressBar.fillAmount = 1;
                yield return new WaitForSeconds(b.cooldown);
            }
            else
            {
                for (float k = 0; k < b.cooldown; k += 0.02f)
                {
                    b.progressBar.fillAmount = k / b.cooldown;
                    yield return new WaitForSeconds(0.02f);
                }
                b.progressBar.fillAmount = 1;
            }
            b.spawnBall = true;
            if (b.autoDrop)
            {
                StartCoroutine(SpawnBall(b));
            }
        }
    }
}
