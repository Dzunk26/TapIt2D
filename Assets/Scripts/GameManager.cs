using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    private const string PLAYER_PREFS_WIN_STRIKE = "WinStrike";
    private const string PLAYER_PREFS_TOTAL_COIN_AMOUNT = "TotalCoinAmount";

    public static GameManager Instance { get; private set; }

    private int totalCointAmount;
    private int coinAmountReceive = 10;
    private int coinAmountInGift = 50;
    private int winStrike;
    private int winStrikeMax = 4;
    public event EventHandler OnIncreaseWinStrike;
    public event EventHandler OnIncreaseTotalCoinAmount;

    private void Awake() {
        Instance = this;

        winStrike = PlayerPrefs.GetInt(PLAYER_PREFS_WIN_STRIKE, 0);
        totalCointAmount = PlayerPrefs.GetInt(PLAYER_PREFS_TOTAL_COIN_AMOUNT, 0);
    }

    private void Start() {
        LevelManager.Instance.OnWinLevel += LevelManager_OnWinLevel;
        WinUI.Instance.OnGetCoin += WinUI_OnGetCoin;
        WinUI.Instance.OnGetMoreCoin += WinUI_OnGetMoreCoin;
    }

    private void WinUI_OnGetMoreCoin(object sender, System.EventArgs e) {
        int newCoinAmountReceive = coinAmountReceive * GetTimesCoinReceive();
        IncreaseTotalCoinAmount(newCoinAmountReceive);
    }

    private void WinUI_OnGetCoin(object sender, System.EventArgs e) {
        IncreaseTotalCoinAmount(coinAmountReceive);
    }

    private void LevelManager_OnWinLevel(object sender, System.EventArgs e) {
        HandleIncreaseWinStrike();

        if (winStrike >= winStrikeMax) {
            winStrike = 0;

            IncreaseTotalCoinAmount(coinAmountInGift);
        }
    
    }

    private void HandleIncreaseWinStrike() {
        winStrike++;

        OnIncreaseWinStrike?.Invoke(this, EventArgs.Empty);

        if (winStrike >= winStrikeMax) {
            winStrike = 0;

            IncreaseTotalCoinAmount(coinAmountInGift);
        }

        PlayerPrefs.SetInt(PLAYER_PREFS_WIN_STRIKE, winStrike);
        PlayerPrefs.Save();
    }

    public float GetWinStrikeNormalized() {
        return (float)winStrike / winStrikeMax;
    }

    public int GetCoinAmountReceive() {
        return coinAmountReceive;
    }

    public int GetTotalCoinAmount() {
        return totalCointAmount;
    }

    public void IncreaseTotalCoinAmount(int coinReceive) {
        totalCointAmount += coinReceive;

        PlayerPrefs.SetInt(PLAYER_PREFS_TOTAL_COIN_AMOUNT, totalCointAmount);
        PlayerPrefs.Save();

        OnIncreaseTotalCoinAmount?.Invoke(this, EventArgs.Empty);
    }

    private int GetTimesCoinReceive() {
        return UnityEngine.Random.Range(2, 5);
    }
}