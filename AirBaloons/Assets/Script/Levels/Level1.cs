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
            //timeActivationDic.Add(1, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(-186f, -2.2f, -222f)));
            //timeActivationDic.Add(2, new FloatItem("Prefabs/Actors/Blimp1", new Vector3(32.7f, -1.7f, 5.5f)));
            timeActivationDic.Add(2, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-26f, 11.3f, 28f)));
            timeActivationDic.Add(3, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-4.4f, 11.3f, 28f)));
            timeActivationDic.Add(4, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(22.2f, 5f, -9.4f)));
            timeActivationDic.Add(5, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-60.8f, 7.5f, -9.4f)));
            timeActivationDic.Add(6, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-19.4f, 34.3f, -9.4f)));
            timeActivationDic.Add(7, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-4.4f, 11.3f, 28f)));
            timeActivationDic.Add(8, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-26f, 11.3f, 28f)));
            timeActivationDic.Add(9, new FloatItem("Prefabs/Actors/Balloon2", new Vector3(-60.8f, 7.5f, -9.4f)));
        }
    }
}
