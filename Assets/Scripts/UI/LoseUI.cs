using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour {
    [SerializeField] private Button retryButton;

    private void Awake() {
        retryButton.onClick.AddListener(() => {
            Hide();
        });
    }

    private void Start() {
        Hide();

        LevelManager.Instance.OnLoseLevel += LevelManager_OnLoseLevel;
    }

    private void LevelManager_OnLoseLevel(object sender, System.EventArgs e) {
        Show();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
