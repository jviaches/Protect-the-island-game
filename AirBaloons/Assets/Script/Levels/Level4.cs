using Assets.Script.Infra;
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

        public int CollectedCoins { get; set; }

        public float BaloonGenerationFrequencyModifier { get { return 1.2f; } }

        public float PlaneGenerationFrequencyModifier { get { return 20; } }

        public int MoneyGenerationModifier { get { return 1; } }

        public List<KeyValuePair<float, FloatItem>> TimeActivationDic { get; private set; }

        public Level4(GameSettings gameSettings)
        {
            TimeActivationDic = new List<KeyValuePair<float, FloatItem>>()
            {
                { new KeyValuePair<float, FloatItem>( 5f, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_3])) },
                { new KeyValuePair<float, FloatItem>( 5f, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_9])) },
                { new KeyValuePair<float, FloatItem>( 7f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_7])) },
                { new KeyValuePair<float, FloatItem>( 12f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_5])) },
                { new KeyValuePair<float, FloatItem>( 15f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_3])) },
                { new KeyValuePair<float, FloatItem>( 19f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_6])) },
                { new KeyValuePair<float, FloatItem>( 23f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_9])) },
                { new KeyValuePair<float, FloatItem>( 27f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_7])) },
                { new KeyValuePair<float, FloatItem>( 31f, new FloatItem("Prefabs/Actors/Blimp1", gameSettings.BornPoints[BornPoint.Clock_7])) },
                { new KeyValuePair<float, FloatItem>( 39f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_9])) },
                { new KeyValuePair<float, FloatItem>( 43f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_6])) },
                { new KeyValuePair<float, FloatItem>( 47f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_7])) },
                { new KeyValuePair<float, FloatItem>( 55f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_3])) },
                { new KeyValuePair<float, FloatItem>( 60f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_9])) },
                { new KeyValuePair<float, FloatItem>( 64f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_9])) },
                { new KeyValuePair<float, FloatItem>( 70f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_6])) },
                { new KeyValuePair<float, FloatItem>( 74f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_7])) },
                { new KeyValuePair<float, FloatItem>( 78f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_5])) },
                { new KeyValuePair<float, FloatItem>( 82f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_3])) },
                { new KeyValuePair<float, FloatItem>( 86f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_9])) },
                { new KeyValuePair<float, FloatItem>( 90f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_7])) },
                { new KeyValuePair<float, FloatItem>( 94f, new FloatItem("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3), gameSettings.BornPoints[BornPoint.Clock_5])) },
            };
        }
    }
}
