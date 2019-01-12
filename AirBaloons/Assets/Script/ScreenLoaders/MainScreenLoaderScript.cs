using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenLoaderScript : MonoBehaviour {

    private Button exitButton;
    private Button playButton;
    private Button settingsButton;

    void Start () {
        exitButton = GameObject.Find("close").GetComponent<Button>();
        playButton = GameObject.Find("play").GetComponent<Button>();
        settingsButton = GameObject.Find("settings").GetComponent<Button>();

        playButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        settingsButton.onClick.AddListener(() => SceneManager.LoadScene("SettingsScene"));
        exitButton.onClick.AddListener(() => Application.Quit());
    }
}
