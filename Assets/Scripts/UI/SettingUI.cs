using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour {
    public const string PLAYER_PREFS_TOGGLE_SOUND_SETTING = "ToggleSound";
    public const string PLAYER_PREFS_TOGGLE_MUSIC_SETTING = "ToggleMusic";
    public const string PLAYER_PREFS_TOGGLE_VIBRATION_SETTING = "ToggleVibration";
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

        toggleSound.onValueChanged.AddListener(OnToggleSoundChanged);
        toggleMusic.onValueChanged.AddListener(OnToggleMusicChanged);
        toggleVibration.onValueChanged.AddListener(OnToggleVibrationChanged);

        LoadSetting();
    }

    private void OnToggleSoundChanged(bool isOn) {
        if (isOn) {
            SoundManager.Instance.SetVolume(SoundManager.Instance.GetDefaultVolume());
        }
        else {
            SoundManager.Instance.SetVolume(0f);
        }

        PlayerPrefs.SetInt(PLAYER_PREFS_TOGGLE_SOUND_SETTING, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnToggleMusicChanged(bool isOn) {
        if (isOn) {
            MusicManager.Instance.SetVolume(SoundManager.Instance.GetDefaultVolume());
        }
        else {
            MusicManager.Instance.SetVolume(0f);
        }

        PlayerPrefs.SetInt(PLAYER_PREFS_TOGGLE_MUSIC_SETTING, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnToggleVibrationChanged(bool isOn) {
        PlayerPrefs.SetInt(PLAYER_PREFS_TOGGLE_VIBRATION_SETTING, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSetting() {
        bool soundSetting = PlayerPrefs.GetInt(PLAYER_PREFS_TOGGLE_SOUND_SETTING, 1) == 1;
        toggleSound.isOn = soundSetting;

        bool musicSetting = PlayerPrefs.GetInt(PLAYER_PREFS_TOGGLE_MUSIC_SETTING, 1) == 1;
        toggleSound.isOn = musicSetting;

        bool vibrationSetting = PlayerPrefs.GetInt(PLAYER_PREFS_TOGGLE_VIBRATION_SETTING, 1) == 1;
        toggleSound.isOn = vibrationSetting;
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