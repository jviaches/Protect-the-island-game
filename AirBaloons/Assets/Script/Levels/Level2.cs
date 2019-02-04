using Assets.Script.Infra;
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

        public float BaloonGenerationFrequencyModifier { get { return 0.95f; } }

        public float PlaneGenerationFrequencyModifier { get { return 40f; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private readonly Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level2()
        {
            timeActivationDic = new Dictionary<float, FloatItem>();
            timeActivationDic.Add(3, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(45f, 10f, 108f)));
        }
    }
}
