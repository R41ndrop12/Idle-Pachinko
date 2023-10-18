using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameRules : MonoBehaviour
{

    /// <summary>
    /// Reference to the GameData passed by the GameManager
    /// </summary>
    GameData _currentGameData;

    public event Action<int, bool> OnModifyManagerAvailability, OnToggleBallActivationState;
    public event Action<int, float> OnStartWorkOnBall;
    public event Action<int> OnActivateBall, OnAutomateBall;
    public event Action<int, GameData> OnUpdateData, OnPerformAction;

    /// <summary>
    /// Handles clicking of the Manager purchase button per each Ball (index)
    /// </summary>
    public void HandleManagerPurchase(int index)
    {
        if (_currentGameData.Managers[index])
            return;
        _currentGameData.Money -= _currentGameData.BallDataList[index].ManagerPrice;
        Debug.Log($"Purchased a manager for {index}");
        ActivateManagerFor(index);
    }

    /// <summary>
    /// Activates the automation of clicking the button - to implement managers
    /// </summary>
    /// <param name="index"></param>
    private void ActivateManagerFor(int index)
    {
        AutomateTask(index);
        SendDataUpdate();
    }

    /// <summary>
    /// Performs the work of "clicking the button" automatically if we have purchasesd the manager
    /// </summary>
    /// <param name="index"></param>
    public void HandleManager(int index)
    {
        if (_currentGameData.Managers[index])
        {
            AutomateTask(index);
        }
    }

    /// <summary>
    /// Performs the work of "clicking the button" automatically
    /// </summary>
    /// <param name="index"></param>
    private void AutomateTask(int index)
    {
        HandleStartBallProgress(index);
    }

    /// <summary>
    /// Logic to unlock the Ball (purchase it) before we can use it to make money
    /// </summary>
    /// <param name="index"></param>
    public void PurchaseBallFirstTime(int index)
    {
        _currentGameData.Money -= _currentGameData.BallDataList[index].BallCooldownUpgradePrice(_currentGameData.BallCooldownCount[index]);
        _currentGameData.BallCooldownCount[index] = 1;
        ActivateBall(index);
    }

    /// <summary>
    /// Activates the Ball that was purchased so that we can click it
    /// </summary>
    /// <param name="i"></param>
    private void ActivateBall(int i)
    {
        OnActivateBall?.Invoke(i);
        SendDataUpdate();
    }

    /// <summary>
    /// Adds money to the data and sends the update event
    /// </summary>
    /// <param name="index"></param>
    public void IncreaseMoney(int index, float multiplier)
    {
        _currentGameData.Money += _currentGameData.BallDataList[index].BallIncome(_currentGameData.BallCooldownCount[index], multiplier);
        SendDataUpdate();
    }

    /// <summary>
    /// Runs the work needed to spawn the ball
    /// </summary>
    /// <param name="index"></param>
    public void HandleStartBallProgress(int index)
    {
        OnPerformAction?.Invoke(index, _currentGameData);
        OnStartWorkOnBall?.Invoke(index, _currentGameData.BallDataList[index].Delay);
    }

    /// <summary>
    /// Handle Upgrading the ball cooldown by spending the money to increas the count
    /// </summary>
    /// <param name="index"></param>
    public void HandleUpgrade(int index)
    {
        _currentGameData.Money -= _currentGameData.BallDataList[index].BallCooldownUpgradePrice(_currentGameData.BallCooldownCount[index]);
        _currentGameData.BallCooldownCount[index] += 1;
        SendDataUpdate();
    }

    /// <summary>
    /// Sends update about data changes as an event
    /// </summary>
    private void SendDataUpdate()
    {
        for (int i = 0; i < _currentGameData.BallDataList.Count; i++)
        {
            UnlockOtherBalls(i);
            UnlockManagers(i);
            CheckBonusCooldownMultiplier(i);
            OnUpdateData?.Invoke(i, _currentGameData);
        }
    }

    /// <summary>
    /// Logic to unlock managers for purchase
    /// </summary>
    /// <param name="index"></param>
    private void UnlockManagers(int index)
    {
        if (_currentGameData.Managers[index] == false)
        {
            bool val =
                _currentGameData.BallDataList[index].ManagerPrice < _currentGameData.Money;
            OnModifyManagerAvailability?.Invoke(index, val);
        }
    }

    /// <summary>
    /// Logic to unlock other balls for purchase when we have enough money
    /// </summary>
    /// <param name="index"></param>
    private void UnlockOtherBalls(int index)
    {
        if (_currentGameData.BallCooldownCount[index] == 0)
        {
            bool val =
                _currentGameData.BallDataList[index].BallCooldownUpgradePrice(_currentGameData.BallCooldownCount[index])
                < _currentGameData.Money;
            OnToggleBallActivationState?.Invoke(index, val);
        }
    }

    /// <summary>
    /// Handles Bonus Cooldown Multiplier so that we have a better balancing 
    /// </summary>
    /// <param name="index"></param>
    private void CheckBonusCooldownMultiplier(int index)
    {
        if (_currentGameData.BallCooldownCount[index]
                    >= _currentGameData.BallDataList[index].MaxCount(_currentGameData.BallBonusCooldownMultiplier[index], _currentGameData.BallCooldownMaxCountHelper[index]))
        {
            _currentGameData.BallBonusCooldownMultiplier[index] -= 0.05f;
            if (_currentGameData.BallBonusCooldownMultiplier[index] >= _currentGameData.BallDataList[index].BonusMaxCountThreshold)
                _currentGameData.BallCooldownMaxCountHelper[index] += _currentGameData.BallDataList[index].MaxCountIncrement;
        }
    }

}
