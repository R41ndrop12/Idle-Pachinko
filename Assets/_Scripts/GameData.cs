using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

[Serializable]
public class GameData : MonoBehaviour
{
    public Data _data = new Data();
    private Data _dataNew = new Data();
    public static event Action saveGame;
    public static event Action loadGame;
    public TMP_InputField saveInput;
    private void Start()
    {
        _dataNew = _data;
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
        Data _dataBackup = new Data();
        _dataBackup = _data;
        if (saveInput.text.Equals(""))
        {
            saveInput.text = "Input a save code first!";
        }
        else
        {
            try
            {
                Data _dataTemp = new Data();
                string decrypted = Decode(saveInput.text);
                _dataTemp = JsonUtility.FromJson<Data>(decrypted);
                Compare(_data, _dataTemp);
            }
            catch
            {
                saveInput.text = "Input a valid save code!";
                _data = _dataBackup;
            }
        }
        loadGame?.Invoke();
    }

    public void Delete()
    {
        _data = _dataNew;
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

    private void Compare(Data data, Data temp)
    {
        data.Money = temp.Money;
        data.RunMoney = temp.RunMoney;
        data.LifetimeMoney = temp.LifetimeMoney;
        if(data.balls.Count != temp.balls.Count)
        {
            for (int i = 0; i < temp.balls.Count; i++)
            {
                data.balls[i] = temp.balls[i];
            }
        }
        else
        {
            data.balls = temp.balls;
        }
    }

}

[System.Serializable]
public class Data
{
    //Money representation should handle a lot but at some point it will get reset to 0
    public double Money;
    public double RunMoney;
    public double LifetimeMoney;

    public List<BallData> balls = new List<BallData>();    
}
 

[System.Serializable]
public class BallData
{
    public double money;
    public Transform spawnPoint;
    public float spawnLimit;
    public GameObject ball;
}

