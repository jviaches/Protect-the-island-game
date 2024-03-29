﻿using Assets.Script.Infra;
using Assets.Script.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Levels
{
    public class Level2 : ILevel
    {
        public int LevelIndex { get { return 2; } }

        public int CollectedCoins { get; set; }

        public float BaloonGenerationFrequencyModifier { get { return 1.2f; } }

        public float PlaneGenerationFrequencyModifier { get { return 20; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private List<KeyValuePair<float, FloatItem>>  timeActivationDic;
        public List<KeyValuePair<float, FloatItem>> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level2(GameSettings gameSettings)
        {
            timeActivationDic = new List<KeyValuePair<float, FloatItem>>()
            {
                { new KeyValuePair<float, FloatItem>( 30f, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_3])) },
                { new KeyValuePair<float, FloatItem>( 60f, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_6])) } 
            };
        }
    }
}
