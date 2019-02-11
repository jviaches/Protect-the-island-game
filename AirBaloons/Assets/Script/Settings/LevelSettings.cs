using Assets.Script.Levels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Settings;

public class LevelSettings : MonoBehaviour
{
    public int LevelsInEpisode = 10;
    public float LevelTimer = 5;
    //public int SelectedLevelIndex = 1;                   // Level chosen by user
    public List<ILevel> Episode1Levels;

    void Awake()
    {
        var gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        Episode1Levels = new List<ILevel>
        {
            new Level1(gameSettings),
            new Level2(gameSettings),
            new Level3(gameSettings),
            new Level4(gameSettings),
            new Level5(gameSettings),
            new Level6(gameSettings),
            new Level7(gameSettings),
            new Level8(gameSettings),
            new Level9(gameSettings),
            new Level10(gameSettings),
        };

        SelectedLevel = Episode1Levels[0];
    }

    // Persistance 
    private int lastCompletedLevelIndex = 0;
    public int LastCompletedLevelIndex
    {
        get { return lastCompletedLevelIndex; }
        set { lastCompletedLevelIndex = value; }
    }

    public ILevel SelectedLevel
    {
        get;
        set;
    }

    public void PrepareNextLevel(int level)
    {
        if (level < Episode1Levels.Count)
        {
            SelectedLevel = Episode1Levels[level - 1];

            LastCompletedLevelIndex = (LastCompletedLevelIndex >= SelectedLevel.LevelIndex) ? LastCompletedLevelIndex : SelectedLevel.LevelIndex;
        }
    }
}