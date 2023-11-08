using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Mode
{
    One,
    Ten,
    Hundred,
    Max,
    Next
}
public class BuyMode : MonoBehaviour
{

    public static event Action<Mode> changeBuyMode;
    public TextMeshProUGUI modeText;
    public Mode buyMode = Mode.Ten;
    public void ChangeBuyMode()
    {
        switch(buyMode)
        {
            case Mode.Ten:
                modeText.SetText("x10");
                changeBuyMode?.Invoke(buyMode);
                buyMode = Mode.Hundred;
                break;
            case Mode.Hundred:
                modeText.SetText("x100");
                changeBuyMode?.Invoke(buyMode);
                buyMode = Mode.Max;
                break;
            case Mode.Max:
                modeText.SetText("MAX");
                changeBuyMode?.Invoke(buyMode);
                buyMode = Mode.Next;
                break;
            case Mode.Next:
                modeText.SetText("NEXT");
                changeBuyMode?.Invoke(buyMode);
                buyMode = Mode.One;
                break;
            default:
                modeText.SetText("x1");
                changeBuyMode?.Invoke(buyMode);
                buyMode = Mode.Ten;
                break;
        }
    }
}
