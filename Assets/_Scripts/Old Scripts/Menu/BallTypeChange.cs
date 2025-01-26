using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BallType
{
    Normal = 0,
    Bouncy = 1,
    Gamble = 2,
    Dio = 3,
    Float = 4,
    Splash = 5
}
public class BallTypeChange : MonoBehaviour
{
    public static event Action<int> changeBallType;
    public TextMeshProUGUI modeText;
    public BallType ballType = BallType.Normal;
    public void ChangeBallType()
    {
        switch(ballType)
        {
            case BallType.Splash:
                modeText.SetText("Float");
                ballType = BallType.Float;
                break;
            case BallType.Float:
                modeText.SetText("Dio");
                ballType = BallType.Dio;
                break;
            case BallType.Dio:
                modeText.SetText("Gamble");
                ballType = BallType.Gamble;
                break;
            case BallType.Gamble:
                modeText.SetText("Bouncy");
                ballType = BallType.Bouncy;
                break;
            case BallType.Bouncy:
                modeText.SetText("Normal");
                ballType = BallType.Normal;
                break;
            default:
                modeText.SetText("Splash");
                ballType = BallType.Splash;
                break;
        }
        changeBallType?.Invoke((int)ballType);
    }
}
