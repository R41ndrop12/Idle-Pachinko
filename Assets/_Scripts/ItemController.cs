using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private ProgressBar _progressBar;
    [SerializeField]
    private ProgressButton _progressButton;
    [SerializeField]
    private TextMeshProUGUI _itemMoney;
    [SerializeField]
    private Button _buyButton;
    [SerializeField]
    private TextMeshProUGUI _buyCooldownButtonText;
    [SerializeField]
    private TextMeshProUGUI _itemCooldownCount;
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private UIPurchaseInfo _purchaseCooldownInfo;
    [SerializeField]
    private GameObject _buyCooldownPanel;

    public event Action OnProgressButtonClicked, OnWorkFinished, OnBuyButtonClicked, OnFirstActivation;

    public bool isWorking => _progressButton.IsEnabled == false;

    private void Awake()
    {
        _progressButton.OnButtonClicked.AddListener(HandleFirstClick);
        _progressBar.OnProgressBarFinished.AddListener(HandleProgressBarFinished);
        _buyButton.onClick.AddListener(HandleBuyButton);
        ToggleBuyButton(false);
        _progressButton.IsEnabled = false;
        ToggleIncome(false);
        _progressButton.SwapSpriteToInactive();
    }

    public void Prepare(Sprite icon)
    {
        _itemImage.sprite = icon;
    }

    public void ActivateButton()
    {
        ToggleIncome(true);
        _purchaseCooldownInfo.gameObject.SetActive(false);
        _progressBar.gameObject.SetActive(true);
        _buyCooldownPanel.SetActive(true);
        _progressButton.IsEnabled = true;
        _progressButton.SwapSpriteToDefault();
        _progressButton.OnButtonClicked.RemoveAllListeners();
        _progressButton.OnButtonClicked.AddListener(HandleProgressButtonClick);
    }

    internal void ResetEvents()
    {
        OnProgressButtonClicked = null;
        OnWorkFinished = null;
        OnBuyButtonClicked = null;
        OnFirstActivation = null;
    }

    public void ToggleActivation(bool val)
    {
        if (val)
        {
            _progressButton.IsEnabled = true;
            _progressButton.SwapSpriteToPrchasable();
            _purchaseCooldownInfo.SwapImageReady();
        }
        else
        {
            _progressButton.IsEnabled = false;
            _progressButton.SwapSpriteToInactive();
            _purchaseCooldownInfo.SwapImageNotReady();
        }

    }

    public void AutomateButton()
    {
        _progressButton.IsEnabled = false;
        OnWorkFinished = null;
        _progressButton.SwapSpriteToDefault();
        _progressBar.ResetProgress();
    }

    private void HandleFirstClick()
    {
        OnFirstActivation?.Invoke();
        ActivateButton();
    }

    private void HandleProgressButtonClick()
    {
        OnProgressButtonClicked?.Invoke();
    }

    private void HandleBuyButton()
        => OnBuyButtonClicked?.Invoke();

    public void SetBuyPrice(double price)
    {
        if (_purchaseCooldownInfo.isActiveAndEnabled)
        {
            _purchaseCooldownInfo.SetPrice($"{price.ToString("F2")} $");
            return;
        }
        _buyCooldownButtonText.text = $"{price.ToString("F2")} $";
    }

    public void SetItemCount(int count, int maxCount)
        => _itemCooldownCount.text = $"{count} / {maxCount}";

    public void ToggleBuyButton(bool val)
        => _buyButton.interactable = val;

    public void StartWork(float delay)
    {
        _progressButton.IsEnabled = false;
        _progressButton.SwapSpriteToInactive();
        _progressBar.RunProgressBar(delay);
    }

    public void ToggleIncome(bool val)
        => _itemMoney.gameObject.SetActive(val);
    public void SetIncome(double score)
    {
        _itemMoney.text = $"{score.ToString("F2")} $";
    }

    private void HandleProgressBarFinished()
    {
        _progressButton.IsEnabled = true;
        _progressButton.SwapSpriteToDefault();
        OnWorkFinished?.Invoke();
    }

    private void OnDisable()
    {
        _progressButton.OnButtonClicked.RemoveListener(HandleProgressButtonClick);
        _progressBar.OnProgressBarFinished.RemoveListener(HandleProgressBarFinished);
        _buyButton.onClick.RemoveListener(HandleBuyButton);
    }
}
