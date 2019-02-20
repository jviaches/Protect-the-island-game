using Assets.Script.Levels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Assets.Script.Settings
{
    public class GameSettings: MonoBehaviour
    {
        public LevelSettings LevelSettings;
        public UpgradeSettings UpgradeSettings;

        public event EventHandler EnemySelected;

        private GameObject selectedEnemy;
        public GameObject SelectedEnemy
        {
            get { return selectedEnemy; }
            set
            {
                selectedEnemy = value;
                EnemySelected.Invoke(this, new EventArgs());
            }
        }

        private ILevel currentLevel;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            LevelSettings = gameObject.GetComponent<LevelSettings>();
            currentLevel = LevelSettings.SelectedLevel;
            //print("LevelSettings " + LevelSettings);

            BalloonHealth = 10 + (LevelSettings.SelectedLevel.LevelIndex * 2);
            ZeppelinHealth = 80 + (LevelSettings.SelectedLevel.LevelIndex * 10);
            BallonsGenerationFrequensy = 5f;//0.1f - (LevelSettings.SelectedLevel.LevelIndex * 0.05f);
            PlanesBornPosition = BornPoints[BornPoint.Clock_3];

            LoadData();
        }

        public Dictionary<BornPoint, Vector3> BornPoints = new Dictionary<BornPoint, Vector3>
        {
            { BornPoint.Clock_3, new Vector3(-80f, 5f, -9.4f) },
            { BornPoint.Clock_5, new Vector3(-24.2f, 11.3f, 28f) },
            { BornPoint.Clock_6, new Vector3(-18.5f, 0f, 30f) },
            { BornPoint.Clock_7, new Vector3(0f, 0f, 30f) },
            { BornPoint.Clock_9, new Vector3(30f, 5f, -9.4f) }
        };

        public Vector3 PlanesBornPosition;

        public int BaseIslandHealth = 100;

        public float ZeppelinSpeed = 1f;

        private readonly float baseBaloonsSpeed = 3f;
        public float BaloonsSpeed
        {
            get
            {
                if (IsSpeedSlownessBuffOn)
                    return baseBaloonsSpeed + (LevelSettings.SelectedLevel.LevelIndex * 0.2f) - SpeedSlownessBuffModifier;
                else
                    return baseBaloonsSpeed + (LevelSettings.SelectedLevel.LevelIndex * 0.2f);
            }
        }

        public int BalloonHealth;
        public int ZeppelinHealth;

        public int PlayerClickDamage = 10;

        public float BallonsGenerationFrequensy;
        public float PlanesGenerationFrequensy = 30f;

        public bool IsMoneyIncreaseBuffOn = false;
        public int MoneyIncreaseBuffMultiplayer = 4;
        public float MoneyIncreaseBuffProbability = 0.01f;

        public bool IsSpeedSlownessBuffOn = false;
        public float SpeedSlownessBuffModifier = 0.5f;
        public float SpeedSlownessBuffProbability = 0.01f;

        // Settings Screen configurations
        public bool IsLevelGuidenceOn = false;
        public bool IsMusicMuted = false;

        public float MusicLevel = 0;
        public float FXsoundLevel = 0;

        #region Persistance

        public void LoadData()
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

                DataSettings dataSettings = bf.Deserialize(file) as DataSettings;
                file.Close();

                // level settings
                LevelSettings.LastCompletedLevelIndex = dataSettings.LastCompletedLevelIndex;                

                for (int lvlRecord = 0; lvlRecord < dataSettings.LevelRecords.Count; lvlRecord++)
                    LevelSettings.Episode1Levels[lvlRecord].CollectedCoins = dataSettings.LevelRecords[lvlRecord].Coins;

                int lvlIndex = LevelSettings.LastCompletedLevelIndex == 0 ? 0 : LevelSettings.LastCompletedLevelIndex - 1;
                LevelSettings.SelectedLevel = LevelSettings.Episode1Levels[lvlIndex];

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

        public void SaveData()
        {
            string fileName = Application.persistentDataPath + "/playerInfo.dat";

            if (File.Exists(fileName))
                File.Delete(fileName);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileName, FileMode.CreateNew);

            // Save Level related settings
            var lvlRecords = new List<DataSettings.LevelData>();

            foreach (var lvlData in LevelSettings.Episode1Levels)
                lvlRecords.Add(new DataSettings.LevelData() { LevelIndex = lvlData.LevelIndex, Coins = lvlData.CollectedCoins, });

            var dataSettings = new DataSettings()
            {
                //Lives = PlayerSettings.PlayerLivesAmount,
                LastCompletedLevelIndex = LevelSettings.LastCompletedLevelIndex,
                LevelRecords = lvlRecords,                

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
            public List<LevelData> LevelRecords;
            public SystemDataSettings systemDataSettings;

            [Serializable]
            public class LevelData
            {
                public int LevelIndex;
                public int Coins;
            //    public int Score;
            }

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