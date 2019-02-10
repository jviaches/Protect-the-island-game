using Assets.Script.Infra;
using Assets.Script.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Levels
{
    public class Level5 : ILevel
    {
        public int LevelIndex { get { return 5; } }

        public float BaloonGenerationFrequencyModifier { get { return 1.2f; } }

        public float PlaneGenerationFrequencyModifier { get { return 20; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private List<KeyValuePair<float, FloatItem>> timeActivationDic;
        public List<KeyValuePair<float, FloatItem>> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level5(GameSettings gameSettings)
        { 
            timeActivationDic = new List<KeyValuePair<float, FloatItem>>()
            {
                //{ gameSettings.LevelSettings.LevelTimer / 2f, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_6]) },          // after 1/3 level time
                //{ gameSettings.LevelSettings.LevelTimer / 2f + 10, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_3]) }
            };
        }
    }
}
