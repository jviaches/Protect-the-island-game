using Assets.Script.Infra;
using Assets.Script.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Levels
{
    public class Level3 : ILevel
    {
        public int LevelIndex { get { return 3; } }

        public float BaloonGenerationFrequencyModifier { get { return 0.9f; } }

        public float PlaneGenerationFrequencyModifier { get { return 40; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private readonly Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level3()
        {
            timeActivationDic = new Dictionary<float, FloatItem>
            {
                { LevelSettings.LevelTimer / 3.33f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_5]) },       // after 1/3 level time
                { LevelSettings.LevelTimer / 3.33f + 3, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_7]) }   // after 1/3 level time + 3 sec
            };
        }
    }
}
