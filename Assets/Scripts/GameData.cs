using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    public float money = 0f;
    public Ball[] balls;
    public bool save = false;
    
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
    }

    private void Update()
    {
        if (save)
        {
            SaveGame();
            save = false;
        }
    }
}
