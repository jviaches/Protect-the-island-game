using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Assets.Script.Settings
{
    public static class GameSettings
    {
        //public static readonly float BaloonsBornRadius = 40;    //point on the surface of a sphere with radius 40

        public static readonly Dictionary<BornPoint, Vector3> BornPoints = new Dictionary<BornPoint, Vector3>
        {
            { BornPoint.Clock_3, new Vector3(-75f, 6.7f, -9.4f) },
            { BornPoint.Clock_5, new Vector3(-24.2f, 11.3f, 28f) },
            { BornPoint.Clock_6, new Vector3(-19.4f, 31f, -9.4f) },
            { BornPoint.Clock_7, new Vector3(-5.7f, 11.3f, 26.8f) },
            { BornPoint.Clock_9, new Vector3(20.6f, 5f, -9.4f) }
        };

        public static readonly Vector3 PlanesBornPosition = BornPoints[BornPoint.Clock_3];

        public static int BaseIslandHealth = 100;

        public static float ZeppelinSpeed = 1f;

        private static readonly float baseBaloonsSpeed = 3f;
        public static float BaloonsSpeed
        {
            get
            {
                if (IsSpeedSlownessBuffOn)
                    return baseBaloonsSpeed + (LevelSettings.GetCurrentLevel().LevelIndex * 0.2f) - SpeedSlownessBuffModifier;
                else
                    return baseBaloonsSpeed + (LevelSettings.GetCurrentLevel().LevelIndex * 0.2f);
            }
        }

        public static int BalloonHealth = 10 + (LevelSettings.GetCurrentLevel().LevelIndex * 2);
        public static int ZeppelinHealth = 80 + (LevelSettings.GetCurrentLevel().LevelIndex * 10);

        public static int PlayerClickDamage = 10;

        public static readonly float BallonsGenerationFrequensy = 0.1f - (LevelSettings.GetCurrentLevel().LevelIndex * 0.05f);
        public static readonly float PlanesGenerationFrequensy = 30f;

        public static bool IsMoneyIncreaseBuffOn = false;
        public static readonly int MoneyIncreaseBuffMultiplayer = 4;
        public static readonly float MoneyIncreaseBuffProbability = 0.01f;

        public static bool IsSpeedSlownessBuffOn = false;
        public static readonly float SpeedSlownessBuffModifier = 0.5f;
        public static readonly float SpeedSlownessBuffProbability = 0.01f;

        // Settings Screen configurations
        public static bool IsTutotrialOn = false;
        public static bool IsMusicMuted = false;

        public static float MusicLevel = 0;
        public static float FXsoundLevel = 0;

        static GameSettings()
        {


            //loadData();
        }

        #region Persistance

        private static void loadData()
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

                DataSettings dataSettings = bf.Deserialize(file) as DataSettings;
                file.Close();

                // level settings
                //LevelSettings.LastCompletedLevelIndex = dataSettings.LastCompletedLevelIndex;
                //foreach (var lvlRecord in dataSettings.LevelRecords)
                //{
                //    var levelData = LevelSettings.Episode1Levels.First(rec => rec.Key.LevelIndex == lvlRecord.LevelIndex);
                //    LevelSettings.Episode1Levels[levelData.Key] = new KeyValuePair<int, int>(lvlRecord.Coins, lvlRecord.Score);
                //}

                // player settings
                //PlayerSettings.PlayerLivesAmount = dataSettings.Lives;

                // system settings
                IsMusicMuted = dataSettings.systemDataSettings.IsSoundMuted;
                MusicLevel = dataSettings.systemDataSettings.MusicSound;
                FXsoundLevel = dataSettings.systemDataSettings.FXsound;
            }
            else
            {
                SaveData();
            }
        }

        public static void SaveData()
        {
            string fileName = Application.persistentDataPath + "/playerInfo.dat";

            if (File.Exists(fileName))
                File.Delete(fileName);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileName, FileMode.CreateNew);

            // Save Level related settings
            //var lvlRecords = new List<DataSettings.LevelData>();

            //foreach (var lvlData in LevelSettings.Episode1Levels)
            //    lvlRecords.Add(new DataSettings.LevelData() { LevelIndex = lvlData.Key.LevelIndex, Coins = lvlData.Value.Key, Score = lvlData.Value.Value });

            var dataSettings = new DataSettings()
            {
                //Lives = PlayerSettings.PlayerLivesAmount,
                LastCompletedLevelIndex = LevelSettings.GetCurrentLevel().LevelIndex,
                //LevelRecords = lvlRecords,

                systemDataSettings = new DataSettings.SystemDataSettings()
                {
                    IsSoundMuted = IsMusicMuted,
                    MusicSound = MusicLevel,
                    FXsound = FXsoundLevel,
                    //Language = "English"
                }
            };

            // Save System related settings

            bf.Serialize(file, dataSettings);
            file.Close();
        }
        #endregion

        [Serializable]
        private class DataSettings
        {
            public int LastCompletedLevelIndex;
            //public int Lives;
            //public List<LevelData> LevelRecords;
            public SystemDataSettings systemDataSettings;

            //[Serializable]
            //public class LevelData
            //{
            //    public int LevelIndex;
            //    public int Coins;
            //    public int Score;
            //}

            [Serializable]
            public class SystemDataSettings
            {
                public bool IsSoundMuted;
                public float MusicSound;
                public float FXsound;
                //public string Language;
            }
        }
    }
}