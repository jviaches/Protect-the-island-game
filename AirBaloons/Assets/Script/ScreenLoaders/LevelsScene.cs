using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelsScene : MonoBehaviour
{
    private Dictionary<int, Button> buttonLevelRelation;

    void Awake()
    {
        buttonLevelRelation = new Dictionary<int, Button>();

        // set all levels unavialables by default
        for (int i = 1; i <= LevelSettings.LevelsInEpisode; i++)
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
