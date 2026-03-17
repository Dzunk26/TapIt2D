using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour {
    public static SettingUI Instance { get; private set; }
    [SerializeField] private Toggle toggleVibration;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleSound;
    [SerializeField] private Button buttonResume;

    private void Awake() {
        Instance = this;

        buttonResume.onClick.AddListener(() => { 
            Hide();
        });

    }

    private void Start() {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}