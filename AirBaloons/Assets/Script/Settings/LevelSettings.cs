using Assets.Script.Levels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class LevelSettings
{
    public static int LevelsInEpisode = 16;
    
    public static Dictionary<ILevel, int> Episode1Levels = new Dictionary<ILevel, int>();   // 1rs param level, 2nd param amount of stars
    public static int SelectedLevelIndex = 1;                   // Level chosen by user

    // Persistance 
    private static int lastCompletedLevelIndex = 0;
    public static int LastCompletedLevelIndex
    {
        get { return lastCompletedLevelIndex; }
        set { lastCompletedLevelIndex = value; }
    }


    static LevelSettings()
    {
        if (Episode1Levels.Count > 0)
            return;

        InitEpisode1();
    }

    private static void InitEpisode1()
    {
        Episode1Levels.Add(new Level1(), 0);
        Episode1Levels.Add(new Level2(), 0);
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