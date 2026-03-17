using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectVolume";
    public static SoundManager Instance;
    private float volume = 0.5f;
    private float defaultVolume = 0.5f;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.5f);
    }

    private void Start() {
        Block.OnAnyBlockTapedSound += Block_OnAnyBlockTaped;
        LevelManager.Instance.OnWinLevelSound += LevelManager_OnWinLevelSound;
        LevelManager.Instance.OnLoseLevelSound += LevelManager_OnLoseLevelSound;
        Saw.OnAnyBlockSawedSound += Saw_OnAnyBlockSawedSound;
        Bomb.OnAnyBombExplodeSound += Bomb_OnAnyBombExplodeSound;
    }

    private void Bomb_OnAnyBombExplodeSound(object sender, System.EventArgs e) {
        Bomb bomb = (Bomb)sender;
        PlaySound(audioClipRefsSO.explode, bomb.transform.position);
    }

    private void Saw_OnAnyBlockSawedSound(object sender, System.EventArgs e) {
        Saw saw = (Saw)sender;
        PlaySound(audioClipRefsSO.saw, saw.transform.position);
    }

    private void LevelManager_OnLoseLevelSound(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.lose, LevelManager.Instance.transform.position);
    }

    private void LevelManager_OnWinLevelSound(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.win, LevelManager.Instance.transform.position);
    }

    private void Block_OnAnyBlockTaped(object sender, System.EventArgs e) {
        Block block = (Block)sender;
        PlaySound(audioClipRefsSO.touch, block.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void SetVolume(float volume) {
        this.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetDefaultVolume() {
        return defaultVolume;
    }
}
