using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private const string PLAYER_PREFS_CURRENT_LEVEL = "CurrentLevel";
    public static LevelManager Instance { get; private set; }

    public event EventHandler OnLoadLevel;
    public event EventHandler OnWinLevel;
    public event EventHandler OnLoseLevel;
    public event EventHandler OnWinLevelSound;
    public event EventHandler OnLoseLevelSound;

    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private List<LevelData> leveDataList;
    private List<Block> remainBlocks = new List<Block>();
    private int currentLevel;
    private int levelMax = 10;
    private int MovesAdded = 10;
    private int currentMoves;

    private void Awake() {
        Instance = this;

        currentLevel = PlayerPrefs.GetInt(PLAYER_PREFS_CURRENT_LEVEL, 1);
    }

    private void Start() {
        Block.OnBlockDestroyed += Block_OnBlockDestroy;
        GamePlayUi.Instance.OnReplay += GamePlayUI_OnReplay;
        GamePlayUi.Instance.OnMoveAdded += GamePlayUI_OnMoveAdded;

        LoadLevel();
    }

    private void GamePlayUI_OnMoveAdded(object sender, EventArgs e) {
        currentMoves += MovesAdded;
    }

    private void GamePlayUI_OnReplay(object sender, EventArgs e) {
        LoadLevel();
    }

    private void Block_OnBlockDestroy(object sender, Block.OnBlockDestroyedEventArgs e) {
        if (remainBlocks.Contains(e.destroyedBlock)) {
            remainBlocks.Remove(e.destroyedBlock);
        }

        if (CheckWin()) {
            OnWinLevel?.Invoke(this, EventArgs.Empty);
            OnWinLevelSound?.Invoke(this, EventArgs.Empty);

            int newLevel = currentLevel + 1;
            if (newLevel > levelMax) newLevel = 1;
            SetCurrentLevel(newLevel);

            LoadLevel();
        }

        if (CheckLose()) {
            OnLoseLevel?.Invoke(this, EventArgs.Empty);
            OnLoseLevelSound?.Invoke(this, EventArgs.Empty);

            LoadLevel();
        }
    }

    public void SetCurrentLevel(int currentLevel) {
        this.currentLevel = currentLevel;

        PlayerPrefs.SetInt(PLAYER_PREFS_CURRENT_LEVEL, currentLevel);
        PlayerPrefs.Save();
    }

    public int GetCurrentLevel() {
        return currentLevel;
    }

    public LevelData GetCurrentLevelData() {
        return leveDataList[currentLevel-1];
    }

    public void DecreaseCurrentMove() {
        currentMoves--;
    }

    public int GetCurrentMoves() {
        return currentMoves;
    }

    private bool CheckWin() {
        return remainBlocks.Count == 0;
    }

    private bool CheckLose() {
        if (CheckWin()) return false;

        if (remainBlocks.Count > 0 && currentMoves == 0) return true;

        foreach (Block block in remainBlocks) {
            if (block.CanMove()) return false;
        }
        return true;
    }

    private void LoadLevel() {
        levelLoader.LoadLevel(GetCurrentLevelData());
        currentMoves = GetCurrentLevelData().moveAmount;
        remainBlocks.Clear();
        remainBlocks = new List<Block>(levelLoader.GetBlockList());

        OnLoadLevel?.Invoke(this, EventArgs.Empty);
    }

    public List<Block> GetRemainBlockList() {
        return remainBlocks;
    }
}
