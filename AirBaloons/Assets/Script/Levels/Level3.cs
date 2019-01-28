using Assets.Script.Infra;
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

        private Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level3()
        {
            timeActivationDic = new Dictionary<float, FloatItem>();
            timeActivationDic.Add(3, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(45f, 10f, 108f)));
        }
    }
}
