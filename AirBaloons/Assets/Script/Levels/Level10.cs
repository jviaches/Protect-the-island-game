using Assets.Script.Infra;
using Assets.Script.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Levels
{
    public class Level10 : ILevel
    {
        public int LevelIndex { get { return 10; } }

        public float BaloonGenerationFrequencyModifier { get { return 1.2f; } }

        public float PlaneGenerationFrequencyModifier { get { return 20; } }

        public int MoneyGenerationModifier { get { return 1; } }

        private readonly Dictionary<float, FloatItem> timeActivationDic;
        public Dictionary<float, FloatItem> TimeActivationDic
        {
            get { return timeActivationDic; }
        }

        public Level10()
        {
            timeActivationDic = new Dictionary<float, FloatItem>
            {
                { 3f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_3]) },      // after 1/3 level time
                { 3.1f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_5]) },      // after 1/3 level time
                { 3.2f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_7]) },      // after 1/3 level time

                { LevelSettings.LevelTimer / 3.33f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_3]) },      // after 1/3 level time
                { LevelSettings.LevelTimer / 3.35f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_9]) },      // after 1/3 level time
                { LevelSettings.LevelTimer / 6.66f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_5]) },      // after 2/3 level time
                { LevelSettings.LevelTimer / 6.67f, new FloatItem("Prefabs/Actors/Blimp1", GameSettings.BornPoints[BornPoint.Clock_7]) }      // after 2/3 level time            
            };
        }
    }
}
