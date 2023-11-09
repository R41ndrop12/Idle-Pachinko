using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ball", menuName = "Ball")]
public class Ball : ScriptableObject
{
    //BaseCooldown

    [Header("Cooldown Variables")]
    [SerializeField]
    [Range(0.05f, 10)]
    //Speed at which ball cooldown resets
    public float baseCooldown;

    [Space (20)]
    [Header("Multiplier Variables")]
    [SerializeField]
    //Base amount cost
    public float baseMultiplierCost = 50f;
    public float multIncrease = 0.1f;
    public float multCostMultiplier = 1.5f;

    [Space(20)]
    [Header("Other Variables")]
    public bool isEnabled = false;

    public float baseMoney = 1;
    [HideInInspector]
    public float money = 1;

    public GameObject ball;
    public Color ballColor;
    public Image progressBar;

    //Public
    [HideInInspector]
    public float cooldown;
    [HideInInspector]
    public int amountDropped;
    [HideInInspector]
    public float multiplier;
    [HideInInspector]
    public float multiplierCost;
    //[HideInInspector]
    public bool autoDrop = false;
    //[HideInInspector]
    public bool spawnBall = false;
    private void OnEnable()
    {
        resetBall();
    }

    public void enableBall()
    {
        spawnBall = true;
        isEnabled = true;
    }

    public void disableBall()
    {
        spawnBall = false;
        isEnabled = false;
        autoDrop = false;
    }

    public void resetBall()
    {
        amountDropped = 1;
        money = baseMoney;
        if (progressBar != null)
            progressBar.fillAmount = 1;
        disableBall();
    }



}