using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameData : MonoBehaviour
{
    public Data _data = new Data();
    private Data _dataBackup = new Data();
    public static event Action saveGame;
    public static event Action loadGame;
    public TMP_InputField saveInput;
    private void Update()
    {
        
    }
    public void SaveGame()
    {
        saveGame?.Invoke();
        string gameData = JsonUtility.ToJson(_data);
        string encrypted = Encode(gameData);
        saveInput.text = encrypted;
        Debug.Log("Saved!");

    }

    public void LoadGame()
    {
        _dataBackup = _data;
        if (saveInput.text.Equals(""))
        {
            saveInput.text = "Input a save code first!";
        }
        else
        {
            try
            {
                string decrypted = Decode(saveInput.text);
                _data = JsonUtility.FromJson<Data>(decrypted);
            }
            catch
            {
                saveInput.text = "Input a valid save code!";
                _data = _dataBackup;
            }
        }
        _dataBackup = _data;
        loadGame?.Invoke();
    }

    private string Encode(string data)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(data);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        return encodedText;
    }

    private string Decode(string data)
    {
        byte[] decodedBytes = Convert.FromBase64String(data);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        return decodedText;
    }

}

[System.Serializable]
public class Data
{
    //Money representation should handle a lot but at some point it will get reset to 0
    public float Money;
    public float RunMoney;
    public float LifetimeMoney;
    public float NextPrestige;
    public float TotalPrestige;
    //Bonus multiplier allows us to better balance the incremental game
    //https://www.gamedeveloper.com/design/the-math-of-idle-games-part-i
    public int[] BallMultiplierCount;
    public int[] PrestigeUpgradeCount;
    public List<Ball> BallDataList = new();    
}
