using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenLoaderScript : MonoBehaviour {

    private Button exitButton;
    private Button playButton;

    void Start () {
        exitButton = GameObject.Find("close").GetComponent<Button>();
        playButton = GameObject.Find("play").GetComponent<Button>();

        exitButton.onClick.AddListener(() => Application.Quit());
        playButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
    }
}
