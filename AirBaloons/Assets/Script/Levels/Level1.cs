using Assets.Script.Infra;
using Assets.Script.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Levels
{
    public class Level1 : ILevel
    {
        public int LevelIndex { get  { return 1; } }

        public float BaloonGenerationFrequencyModifier { get { return 1.2f; } }

        public float PlaneGenerationFrequencyModifier { get { return 20; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private readonly Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level1()
        {
            timeActivationDic = new Dictionary<float, FloatItem>();

            // test
            timeActivationDic.Add(LevelSettings.LevelTimer / 3.33f, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(45f, 10f, 108f))); // after 1/3 level time
            timeActivationDic.Add(LevelSettings.LevelTimer / 6.66f, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(45f, 10f, 108f))); // after 2/3 level time
        }
    }
}
