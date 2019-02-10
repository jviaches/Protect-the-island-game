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
    private Toggle tutorial_switch;

    private GameSettings gameSettings;

    void Start () {

        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        print(Application.persistentDataPath);

        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

        systemSound = GameObject.Find("switch").GetComponent<Toggle>();
        systemSound.onValueChanged.AddListener((isOn) => AudioListener.pause = !isOn);

        volumeSlider = GameObject.Find("volume_slider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener((isOn) => AudioListener.volume = volumeSlider.value);

        languageDropDwn = GameObject.Find("lang-dropdown").GetComponent<Dropdown>();
        languageDropDwn.interactable = false;

        tutorial_switch = GameObject.Find("tutorial_switch").GetComponent<Toggle>();
        tutorial_switch.isOn = gameSettings.IsTutotrialOn;
        tutorial_switch.onValueChanged.AddListener((isOn) => gameSettings.IsTutotrialOn = isOn);

        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        saveButton.onClick.AddListener(() =>
        {
            // GameSettings.SaveData();
            SceneManager.LoadScene("MainScene");
        }); 
    }
}
