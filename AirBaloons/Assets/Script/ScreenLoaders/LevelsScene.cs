using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelsScene : MonoBehaviour
{
    private Button closebutton;
    private Button settingsbutton;

    void Awake()
    {
        closebutton = GameObject.Find("ExitButton").GetComponent<Button>();
        closebutton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

        settingsbutton = GameObject.Find("square_button_settings").GetComponent<Button>();
        settingsbutton.onClick.AddListener(() => SceneManager.LoadScene("SettingsScene"));

        for (int i = 1; i <= LevelSettings.LevelsInEpisode; i++)
        {
            // player never played before
            if (LevelSettings.LastCompletedLevelIndex == 0)
            {
                GameObject.Find("LevelText1").GetComponent<Text>().enabled = true;  // format: LevelText1
                GameObject.Find("level1").GetComponent<Button>().enabled = true;    // star format: level_1
                GameObject.Find("star1_1").GetComponent<Image>().enabled = false;   // star format: star1_1
                GameObject.Find("star1_2").GetComponent<Image>().enabled = false;   // star format: star1_2
                GameObject.Find("star1_3").GetComponent<Image>().enabled = false;   // star format: star1_3
                GameObject.Find("lock1").GetComponent<Image>().enabled = false;

                Button lvlButton = GameObject.Find("level1").GetComponent<Button>();
                lvlButton.onClick.AddListener(() => { loadLevel(1); }); // TODO: load proper level event
            }

            // enable all played levels
            if (i <= LevelSettings.LastCompletedLevelIndex)
            {
                GameObject.Find("level" + i).GetComponent<Button>().enabled = true;        // star format: level_1
                GameObject.Find("star" + i + "_1").GetComponent<Image>().enabled = true;   // star format: star1_1
                GameObject.Find("star" + i + "_2").GetComponent<Image>().enabled = true;   // star format: star1_2
                GameObject.Find("star" + i + "_3").GetComponent<Image>().enabled = true;   // star format: star1_3
                GameObject.Find("LevelText" + i).GetComponent<Text>().enabled = true;      // format: LevelText1

                GameObject.Find("lock" + i).GetComponent<Image>().enabled = false;

                Button lvlButton = GameObject.Find("level" + i).GetComponent<Button>();
                lvlButton.onClick.AddListener(() => { loadLevel(i); }); // TODO: load proper level event
            }

            // enable next level (to last played one)
            if (i == LevelSettings.LastCompletedLevelIndex + 1)
            {
                print("Next Level " + i);

                GameObject.Find("star" + i + "_1").GetComponent<Image>().enabled = false;   // star format: star1_1
                GameObject.Find("star" + i + "_2").GetComponent<Image>().enabled = false;   // star format: star1_2
                GameObject.Find("star" + i + "_3").GetComponent<Image>().enabled = false;   // star format: star1_3

                GameObject.Find("level" + i).GetComponent<Button>().enabled = true;        // star format: level_1
                GameObject.Find("LevelText" + i).GetComponent<Text>().enabled = true;      // format: LevelText1
                GameObject.Find("lock" + i).GetComponent<Image>().enabled = false;

                Button lvlButton = GameObject.Find("level" + i).GetComponent<Button>();
                lvlButton.onClick.AddListener(() => { loadLevel(i); }); // TODO: load proper level event
            }

            // disable all not played yet levels
            if (i > LevelSettings.LastCompletedLevelIndex + 1)
            {
                GameObject.Find("level" + i).GetComponent<Button>().enabled = false;        // star format: level_1
                GameObject.Find("star" + i + "_1").GetComponent<Image>().enabled = false;   // star format: star1_1
                GameObject.Find("star" + i + "_2").GetComponent<Image>().enabled = false;   // star format: star1_2
                GameObject.Find("star" + i + "_3").GetComponent<Image>().enabled = false;   // star format: star1_3
                GameObject.Find("LevelText" + i).GetComponent<Text>().enabled = false;      // format: LevelText1

                GameObject.Find("lock" + i).GetComponent<Image>().enabled = true;
            }            
        }
    }

    private void loadLevel(int level)
    {
        LevelSettings.SelectedLevelIndex = level;

        print("loadLvl(). Selected level=" + LevelSettings.SelectedLevelIndex);

        SceneManager.LoadScene("GameScene");  // load level
    }
}
