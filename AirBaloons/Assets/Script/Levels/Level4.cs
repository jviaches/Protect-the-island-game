﻿using Assets.Script.Infra;
using Assets.Script.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Levels
{
    public class Level4 : ILevel
    {
        public int LevelIndex { get { return 4; } }

        public float BaloonGenerationFrequencyModifier { get { return 1.2f; } }

        public float PlaneGenerationFrequencyModifier { get { return 20; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private readonly Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level4()
        {
            timeActivationDic = new Dictionary<float, FloatItem>
            {
                { LevelSettings.LevelTimer / 3.33f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_5]) },   // after 1/3 level time
                { LevelSettings.LevelTimer / 2f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_9]) }      // after 1/2 level time
            };

        }
    }
}
