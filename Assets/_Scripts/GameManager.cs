using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameRules _gameRules;
    private GameData _gameData;
    [SerializeField]
    private SaveSystem _saveSystem;
    [SerializeField]
    private GameUI _gameUI;

    [SerializeField]
    private List<BallData> _ballDataList;

    /// <summary>
    /// All the setup happens here
    /// </summary>
    private void Awake()
    {
        PrepareGameData();
        PrepareUI();
        ConnectGameRulesToUI();

        //_gameRules.PrepareGameData(_gameData);

        //We load the saved game data if we have any
        //LoadSavedData();
    }

    /// <summary>
    /// Connecting Game Rule events to Ui so we can click buttons, progress the game and get a visual response in the UI and
    /// a Visualization
    /// </summary>
    private void ConnectGameRulesToUI()
    {
        _gameRules.OnModifyManagerAvailability += _gameUI.UpdateManagerAvailability;
        _gameRules.OnActivateBall += _gameUI.ActivateItem;
        _gameRules.OnStartWorkOnBall += _gameUI.StartWorkOnItem;
        _gameRules.OnToggleBallActivationState += _gameUI.ToggleItemActiveState;
        _gameRules.OnUpdateData += _gameUI.UpdateCooldownUI;    
    }

    /// <summary>
    /// GameData stores the state of our game
    /// </summary>
    private void PrepareGameData()
    {
        _gameData = new();
        _gameData.BallDataList = _ballDataList;
    }

    /// <summary>
    /// UI is separate from the visual aprt. We could have just UI buttons and progress bar with no visuals.
    /// </summary>
    private void PrepareUI()
    {
        _gameUI.PrepareUI(_ballDataList);

        _gameUI.OnProgressButtonClicked += _gameRules.HandleStartBallProgress;
        _gameUI.OnWorkFinished += _gameRules.HandleManager;
        _gameUI.OnBuyButonClicked += _gameRules.HandleUpgrade;
        _gameUI.OnPurchaseItemFirstTime += _gameRules.PurchaseBallFirstTime;
        _gameUI.OnManagerPurchased += _gameRules.HandleManagerPurchase;
    }

    /// <summary>
    /// I have decided that GameManager will know what objects needs to save and load theire data.
    /// SaveSystem just does the Saving work
    /// </summary>
    /*public void SaveGame()
    {
        List<string> dataToSave = new()
        {
            _gameData.GetSaveData(),
            _visualsController.GetSaveData()
        };
        _saveSystem.SaveTheGame(dataToSave);
    }

    /// <summary>
    /// I have decided that GameManager will know what objects needs to save and load theire data.
    /// SaveSystem just does the Saving work
    /// </summary>
    public void LoadSavedData()
    {
        List<string> data = _saveSystem.LoadGame();
        if (data.Count > 0)
        {
            _gameRules.LoadGame(data[0]);
            _visualsController.LoadData(data[1]);
        }
    }

    /// <summary>
    /// Removes the loaded data
    /// </summary>
    public void ResetGame()
    {
        _saveSystem.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}
