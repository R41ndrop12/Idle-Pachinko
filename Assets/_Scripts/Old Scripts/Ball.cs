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
    private int baseCooldownUpgrades = 0;//store
    public int cooldownUpgradesMax = 20;
    public int baseCooldownCost = 0;
    //Increases price of upgrades by n x multiplier
    public float cooldownCostMultiplier = 1.15f;

    [Space (20)]
    [Header("Multiplier Variables")]
    [SerializeField]
    //Base amount cost
    public float baseMultiplierCost = 50f;
    private int baseMultUpgrades = 0;
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
    public float cooldownCost = 0;
    [HideInInspector]
    public int amountDropped;
    [HideInInspector]
    public float multiplier;
    [HideInInspector]
    public float worldMultiplier;
    [HideInInspector]
    public float multiplierCost;
    [HideInInspector]
    public bool autoDrop = false;
    [HideInInspector]
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
        spawnBall = isEnabled;
        money = baseMoney;
        disableBall();
    }



}