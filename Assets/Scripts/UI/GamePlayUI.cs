using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUi : MonoBehaviour {
    public static GamePlayUi Instance { get; private set; }    
    [SerializeField] private Button settingButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button bombButton;
    [SerializeField] private Button addMovesButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI movesAmountText;
    [SerializeField] private TextMeshProUGUI coinAmountText;

    public event EventHandler OnReplay;
    public event EventHandler OnMoveAdded;
    public event EventHandler OnPlaceBomb;

    private void Awake() {
        Instance = this;

        settingButton.onClick.AddListener(() => {
            SettingUI.Instance.Show();
        });

        replayButton.onClick.AddListener(() => {
            OnReplay?.Invoke(this, EventArgs.Empty);
        });

        bombButton.onClick.AddListener(() => {
            // Dat bomb
        });

        addMovesButton.onClick.AddListener(() => {
            OnMoveAdded?.Invoke(this, EventArgs.Empty);

            UpdateVisual();
        });
    }

    private void Start() {
        UpdateVisual();

        GameManager.Instance.OnIncreaseTotalCoinAmount += GameManager_OnIncreaseTotalCoinAmount;
        Block.OnBlockTaped += Block_OnBlockTaped;
        LevelManager.Instance.OnLoadLevel += LevelManager_OnLoadLevel;
    }

    private void LevelManager_OnLoadLevel(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void Block_OnBlockTaped(object sender, EventArgs e) {
        LevelManager.Instance.DecreaseCurrentMove();
        UpdateVisual();
    }

    private void GameManager_OnIncreaseTotalCoinAmount(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    public void UpdateVisual() {
        levelText.text = "Level " + LevelManager.Instance.GetCurrentLevel();
        movesAmountText.text =  "moves " + LevelManager.Instance.GetCurrentMoves();
        coinAmountText.text = GameManager.Instance.GetTotalCoinAmount().ToString();
    }
}