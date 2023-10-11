using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] public float money { get; private set; }
    public TextMeshProUGUI mText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(float m)
    {
        money += m;
        TextManager.displayValue(mText, "$", money);
    }
    public void SubtractMoney(float m)
    {
        money -= m;
        TextManager.displayValue(mText, "$", money);
    }
}
