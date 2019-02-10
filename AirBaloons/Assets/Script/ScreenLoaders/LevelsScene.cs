using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using Assets.Script.Levels;
using Assets.Script.Settings;

public class LevelsScene : MonoBehaviour
{
    private Button closebutton;
    private Button settingsbutton;

    private GameSettings gameSettings;

    void Awake()
    {
        //print("Awake begin => selLvl=" + LevelSettings.SelectedLevelIndex + " LastComplLvl=" + LevelSettings.LastCompletedLevelIndex);

        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        closebutton = GameObject.Find("ExitButton").GetComponent<Button>();
        closebutton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

        settingsbutton = GameObject.Find("square_button_settings").GetComponent<Button>();
        settingsbutton.onClick.AddListener(() => SceneManager.LoadScene("SettingsScene"));

        for (int levelIndex = 1; levelIndex <= gameSettings.LevelSettings.LevelsInEpisode; levelIndex++)
        {
            // player never played before
            if (levelIndex == 1)
            {
                // not played yet
                if (gameSettings.LevelSettings.LastCompletedLevelIndex == 0)
                {
                    GameObject.Find("LevelText1").GetComponent<Text>().enabled = true;  // format: LevelText1
                    GameObject.Find("level1").GetComponent<Button>().enabled = true;    // star format: level_1
                    GameObject.Find("star1_1").GetComponent<Image>().enabled = false;   // star format: star1_1
                    GameObject.Find("star1_2").GetComponent<Image>().enabled = false;   // star format: star1_2
                    GameObject.Find("star1_3").GetComponent<Image>().enabled = false;   // star format: star1_3
                }
                else
                {
                    GameObject.Find("level1").GetComponent<Button>().enabled = true;   // star format: level_1
                    GameObject.Find("star1_1").GetComponent<Image>().enabled = true;   // star format: star1_1
                    GameObject.Find("star1_2").GetComponent<Image>().enabled = true;   // star format: star1_2
                    GameObject.Find("star1_3").GetComponent<Image>().enabled = true;   // star format: star1_3
                }

                GameObject.Find("lock1").GetComponent<Image>().enabled = false;

                Button lvlButton = GameObject.Find("level1").GetComponent<Button>();
                lvlButton.onClick.AddListener(() => { loadLevel(1); });
            }
            else
            {
                // enable all played levels
                if (levelIndex - 1 < gameSettings.LevelSettings.LastCompletedLevelIndex)
                {
                    //print("LastCompletedLevelIndex=" + LevelSettings.LastCompletedLevelIndex);
                    //print("Found lvl " + levelIndex + " to enable");
                    GameObject.Find("level" + levelIndex).GetComponent<Button>().enabled = true;        // star format: level_1
                    GameObject.Find("star" + levelIndex + "_1").GetComponent<Image>().enabled = true;   // star format: star1_1
                    GameObject.Find("star" + levelIndex + "_2").GetComponent<Image>().enabled = true;   // star format: star1_2
                    GameObject.Find("star" + levelIndex + "_3").GetComponent<Image>().enabled = true;   // star format: star1_3
                    GameObject.Find("LevelText" + levelIndex).GetComponent<Text>().enabled = true;      // format: LevelText1

                    GameObject.Find("lock" + levelIndex).GetComponent<Image>().enabled = false;

                    Button lvlButton = GameObject.Find("level" + levelIndex).GetComponent<Button>();
                    int lvl = levelIndex;                                  // have to do this way, because for some reason on invocation levelIndex change its value to last one.
                    lvlButton.onClick.AddListener(() => loadLevel(lvl)); // TODO: load proper level event
                }
                else if (levelIndex - 1 == gameSettings.LevelSettings.LastCompletedLevelIndex)
                {
                    //print("LastCompletedLevelIndex=" + LevelSettings.LastCompletedLevelIndex + " levelIndex=" + levelIndex + " SelLevl=" + LevelSettings.SelectedLevelIndex);
                    GameObject.Find("level" + levelIndex).GetComponent<Button>().enabled = true;        // star format: level_1
                    GameObject.Find("star" + levelIndex + "_1").GetComponent<Image>().enabled = false;   // star format: star1_1
                    GameObject.Find("star" + levelIndex + "_2").GetComponent<Image>().enabled = false;   // star format: star1_2
                    GameObject.Find("star" + levelIndex + "_3").GetComponent<Image>().enabled = false;   // star format: star1_3
                    GameObject.Find("LevelText" + levelIndex).GetComponent<Text>().enabled = true;      // format: LevelText1

                    GameObject.Find("lock" + levelIndex).GetComponent<Image>().enabled = false;

                    Button lvlButton = GameObject.Find("level" + levelIndex).GetComponent<Button>();
                    int lvl = levelIndex;   // have to do this way, because for some reason on invocation levelIndex change its value to last one.
                    lvlButton.onClick.AddListener(() => loadLevel(lvl));
                }
                // disable all not played yet levels
                else if (levelIndex - 1 > gameSettings.LevelSettings.LastCompletedLevelIndex)
                {
                    GameObject.Find("level" + levelIndex).GetComponent<Button>().enabled = false;        // star format: level_1
                    GameObject.Find("star" + levelIndex + "_1").GetComponent<Image>().enabled = false;   // star format: star1_1
                    GameObject.Find("star" + levelIndex + "_2").GetComponent<Image>().enabled = false;   // star format: star1_2
                    GameObject.Find("star" + levelIndex + "_3").GetComponent<Image>().enabled = false;   // star format: star1_3
                    GameObject.Find("LevelText" + levelIndex).GetComponent<Text>().enabled = false;      // format: LevelText1

                    GameObject.Find("lock" + levelIndex).GetComponent<Image>().enabled = true;
                }
            }
        }

        //print("Awake end => selLvl=" + LevelSettings.SelectedLevelIndex + " LastComplLvl=" + LevelSettings.LastCompletedLevelIndex);
    }

    private void loadLevel(int level)
    {
        gameSettings.LevelSettings.SelectedLevelIndex = level;
        //print("After loadLevel() => selLvl=" + LevelSettings.SelectedLevelIndex + " LastComplLvl="+ LevelSettings.LastCompletedLevelIndex);

        SceneManager.LoadScene("GameScene");  // load level
    }
}
