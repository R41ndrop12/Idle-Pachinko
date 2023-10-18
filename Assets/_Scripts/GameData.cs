using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    //Money representation should handle a lot but at some point it will get reset to 0
    public double Money { get; set; }

    //Bonus multiplier allows us to better balance the incremental game
    //https://www.gamedeveloper.com/design/the-math-of-idle-games-part-i
    public List<int> BallCooldownCount = new();
    public List<bool> Managers = new();
    public List<float> BallBonusCooldownMultiplier = new();
    public List<int> BallCooldownMaxCountHelper = new();

    public List<BallData> BallDataList = new();
}
