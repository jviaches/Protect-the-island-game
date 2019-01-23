using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneLoader : MonoBehaviour {

    private Button exitButton;
    private Button saveButton;
    private Toggle systemSound;
    private Slider volumeSlider;
    private Dropdown languageDropDwn;

    void Start () {
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

        systemSound = GameObject.Find("switch").GetComponent<Toggle>();
        systemSound.onValueChanged.AddListener((isOn) => AudioListener.pause = !isOn);

        volumeSlider = GameObject.Find("volume_slider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener((isOn) => AudioListener.volume = volumeSlider.value);

        languageDropDwn = GameObject.Find("lang-dropdown").GetComponent<Dropdown>();
        languageDropDwn.interactable = false;

        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        saveButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));  // TODO: wire to save setting in file
    }
}
