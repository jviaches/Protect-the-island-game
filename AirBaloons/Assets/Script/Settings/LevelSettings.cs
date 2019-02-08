using Assets.Script.Levels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class LevelSettings
{
    public static int LevelsInEpisode = 10;
    public static int LevelTimer = 100; // Time take to level to complete (in sec)

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
        Episode1Levels.Add(new Level3(), 0);
        Episode1Levels.Add(new Level4(), 0);
        Episode1Levels.Add(new Level5(), 0);
        Episode1Levels.Add(new Level6(), 0);
        Episode1Levels.Add(new Level7(), 0);
        Episode1Levels.Add(new Level8(), 0);
        Episode1Levels.Add(new Level9(), 0);
        Episode1Levels.Add(new Level10(), 0);
    }

    public static void RevealNextLevel(int level)
    {
        if (SelectedLevelIndex < Episode1Levels.Count)
        {
            SelectedLevelIndex = level;
            LastCompletedLevelIndex = (LastCompletedLevelIndex >= SelectedLevelIndex) ? LastCompletedLevelIndex : SelectedLevelIndex;
        }
    }

    public static void RunNextLevel(int level)
    {
        if (SelectedLevelIndex < Episode1Levels.Count)
        {
            SelectedLevelIndex = level + 1;
            LastCompletedLevelIndex = (LastCompletedLevelIndex >= SelectedLevelIndex) ? LastCompletedLevelIndex : SelectedLevelIndex;
        }
    }

    public static ILevel GetCurrentLevel()
    {
        int lvlIndex = SelectedLevelIndex == 0 ? 1 : SelectedLevelIndex;  
        return Episode1Levels.Keys.First(lvl => lvl.LevelIndex == lvlIndex);
    }
}