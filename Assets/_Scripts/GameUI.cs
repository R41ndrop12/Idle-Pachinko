using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the information exchange between UI and other scripts
/// </summary>
public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiItemPrefab;
    [SerializeField]
    private Transform _uiItemParent;
    [SerializeField]
    private UIManagerController _managersController;
    private List<ItemController> _uiItemsList = new();
    [SerializeField]
    private MoneyPanel _scorePanel;

    public event Action<int> OnProgressButtonClicked, OnWorkFinished, OnBuyButonClicked, OnPurchaseItemFirstTime, OnManagerPurchased;

    public void PrepareUI(List<BallData> data)
    {
        _uiItemsList.Clear();
        for (int i = 0; i < data.Count; i++)
        {
            ItemController itemController
                = Instantiate(_uiItemPrefab, _uiItemParent).GetComponent<ItemController>();
            itemController.Prepare(data[i].BallImage);
            _uiItemsList.Add(itemController);
            _managersController.AddButton(i, data[i].ManagerPrice);
            ConnectEvents(i, itemController);
        }
        _managersController.OnManagerPurchased += PurchaseManager;
    }

    private void PurchaseManager(int index)
    {
        OnManagerPurchased?.Invoke(index);
    }


    private void ConnectEvents(int i, ItemController itemController)
    {
        itemController.OnProgressButtonClicked += () => OnProgressButtonClicked?.Invoke(i);
        itemController.OnWorkFinished += () => OnWorkFinished?.Invoke(i);
        itemController.OnBuyButtonClicked += () => OnBuyButonClicked?.Invoke(i);
        itemController.OnFirstActivation += () => OnPurchaseItemFirstTime?.Invoke(i);
    }

    public void UpdateManagerAvailability(int index, bool val)
    {
        _managersController.ToggleButton(index, val);
    }

    public void StartWorkOnItem(int index, float delay)
    {
        _uiItemsList[index].StartWork(delay);
    }

    public void ToggleItemActiveState(int index, bool val)
    {
        _uiItemsList[index].ToggleActivation(val);
    }

    public void UpdateCooldownUI(int index, GameData gameData)
    {
        _managersController.SetButtonPurchased(index, gameData.Managers[index]);
        _uiItemsList[index].SetIncome(gameData.BallDataList[index].BallBaseIncome);
        _uiItemsList[index].SetBuyPrice(gameData.BallDataList[index].BallCooldownUpgradePrice(gameData.BallCooldownCount[index]));
        _uiItemsList[index].SetItemCount(gameData.BallCooldownCount[index], gameData.BallDataList[index].MaxCount(gameData.BallBonusCooldownMultiplier[index], gameData.BallCooldownMaxCountHelper[index]));
        _scorePanel.SetScore(gameData.Money);
        _uiItemsList[index].ToggleBuyButton(gameData.Money >= gameData.BallDataList[index].BallCooldownUpgradePrice(gameData.BallCooldownCount[index]));
    }

    internal void ActivateItem(int index)
    {
        _uiItemsList[index].ActivateButton();
    }
}
