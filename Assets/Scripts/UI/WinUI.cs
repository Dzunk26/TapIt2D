using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour {
    public static WinUI Instance { get; private set; }
    [SerializeField] private Button getCoinButton;
    [SerializeField] private Button getMoreCoinButton;
    [SerializeField] private TextMeshProUGUI coinAmountText;
    [SerializeField] private TextMeshProUGUI coinReceiveText;
    [SerializeField] private Image winStrikeFillImage;

    public event EventHandler OnGetCoin;
    public event EventHandler OnGetMoreCoin;

    private void Awake() {
        Instance = this;

        getCoinButton.onClick.AddListener(() => {
            OnGetCoin?.Invoke(this, EventArgs.Empty);

            UpdateVisual();
            Hide();
        });

        getMoreCoinButton.onClick.AddListener(() => {
            OnGetMoreCoin?.Invoke(this, EventArgs.Empty);

            UpdateVisual();
            Hide();
        });
    }

    private void Start() {
        UpdateVisual();
        Hide();

        GameManager.Instance.OnIncreaseWinStrike += GameManager_OnIncreaseWinStrike;
    }

    private void GameManager_OnIncreaseWinStrike(object sender, EventArgs e) {
        Show();

        UpdateVisual();
    }

    private void UpdateVisual() {
        coinAmountText.text = GameManager.Instance.GetTotalCoinAmount().ToString();
        coinReceiveText.text = GameManager.Instance.GetCoinAmountReceive().ToString();

        winStrikeFillImage.fillAmount = GameManager.Instance.GetWinStrikeNormalized();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
