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

        public float BaloonGenerationFrequencyModifier { get { return 0.5f; } }

        public float PlaneGenerationFrequencyModifier { get { return 40; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level1()
        {
            timeActivationDic = new Dictionary<float, FloatItem>();
            timeActivationDic.Add(1, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(-186f, -2.2f, -222f)));
            timeActivationDic.Add(2, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(32.7f, -1.7f, 5.5f)));
        }
    }
}
