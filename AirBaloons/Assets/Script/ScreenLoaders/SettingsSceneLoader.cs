using Assets.Script.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneLoader : MonoBehaviour {

    private Button exitButton;
    private Button saveButton;
    private Toggle systemSound;
    private Slider volumeSlider;
    private Dropdown languageDropDwn;
    private Toggle levelGuidence;

    private GameSettings gameSettings;

    void Start() {

        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

        volumeSlider = GameObject.Find("volume_slider").GetComponent<Slider>();
        volumeSlider.value = gameSettings.MusicLevel;
        volumeSlider.onValueChanged.AddListener((isOn) =>
        {
            AudioListener.volume = volumeSlider.value;
            gameSettings.MusicLevel = volumeSlider.value;
        });

        systemSound = GameObject.Find("switch").GetComponent<Toggle>();
        systemSound.isOn = !gameSettings.IsMusicMuted;

        systemSound.onValueChanged.AddListener((isOn) =>
        {
            AudioListener.pause = !isOn;
            gameSettings.IsMusicMuted = !isOn;

            if (isOn)
                volumeSlider.enabled = true;
            else
                volumeSlider.enabled = false;
        });

        languageDropDwn = GameObject.Find("lang-dropdown").GetComponent<Dropdown>();
        languageDropDwn.interactable = false;

        levelGuidence = GameObject.Find("level-guidence").GetComponent<Toggle>();
        levelGuidence.isOn = gameSettings.IsLevelGuidenceOn;
        levelGuidence.onValueChanged.AddListener((isOn) =>
        {
            gameSettings.IsLevelGuidenceOn = isOn;
        });

        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        saveButton.onClick.AddListener(() =>
        {
            gameSettings.SaveData();
            SceneManager.LoadScene("MainScene");
        }); 
    }
}
