using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    //Money representation should handle a lot but at some point it will get reset to 0
    public float Money { get; set; }
    public float RunMoney { get; set; }
    public float LifetimeMoney { get; set; }
    public int NextPrestige { get; set; }
    public int TotalPrestige { get; set; }
    //Bonus multiplier allows us to better balance the incremental game
    //https://www.gamedeveloper.com/design/the-math-of-idle-games-part-i
    public List<int> BallCooldownCount = new();

    public List<Ball> BallDataList = new();
}
