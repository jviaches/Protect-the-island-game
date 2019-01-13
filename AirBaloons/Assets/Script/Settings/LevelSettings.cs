using Assets.Script.Levels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class LevelSettings
{
    public static int LevelsInEpisode = 16;



    // 1rs param level, 2nd param amount of stars, 3rd param score
    public static Dictionary<ILevel, KeyValuePair<int, int>> Episode1Levels = new Dictionary<ILevel, KeyValuePair<int, int>>();

    public static int SelectedLevelIndex = 0;                   // Level chosen by user

    public static readonly float CollectablesHeight = 24;                // Same high as player to pickup coins, buffs, etc..
    public static readonly float MinimumAsteroidMovingSpeed = 4f;
    public static readonly float MaximumAsteroidMovingSpeed = 8f;

    // Persistance 
    private static int playerLastCompletedLevelIndex = 0;
    public static int PlayerLastCompletedLevelIndex
    {
        get { return playerLastCompletedLevelIndex; }
        set { playerLastCompletedLevelIndex = value; }
    }


    static LevelSettings()
    {
        if (Episode1Levels.Count > 0)
            return;

        InitEpisode1();
    }

    private static void InitEpisode1()
    {
        Episode1Levels.Add(new Level1(), new KeyValuePair<int, int>(0, 0));
        //Episode1Levels.Add(new Level2(), new KeyValuePair<int, int>(0, 0));
    }
    public static int NextLevel(int currLevel)
    {
        if (currLevel + 1 > Episode1Levels.Count)
            return currLevel;

        return ++SelectedLevelIndex;
    }

    public static ILevel GetCurrentLevel()
    {
        int lvlIndex = SelectedLevelIndex == 0 ? 1 : SelectedLevelIndex;    // can be 0 in case if no games were played before
        return Episode1Levels.Keys.First(lvl => lvl.LevelIndex == lvlIndex);
    }
}