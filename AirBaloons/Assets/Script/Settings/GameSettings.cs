using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Settings
{
    public static class GameSettings
    {
        public static readonly float BaloonsBornRadius = 40;    //point on the surface of a sphere with radius 40

        public static readonly Vector3 PlanesBornPosition = new Vector3(-106.5f, 5.8f, -87f);
        public static readonly Vector3[] BalloonsBornPositions = new Vector3[]
        {
            new Vector3(-60.8f, 6.7f, -9.4f),     // 3 oclock
            new Vector3(-24.2f, 11.3f, 28f),      // 5 oclock
            new Vector3(-19.4f, 31f, -9.4f),      // 6 oclock
            new Vector3(-5.7f, 11.3f, 26.8f),     // 7 oclock
            new Vector3(20.6f, 5f, -9.4f),        // 9 oclock
        };

        public static int BaseIslandHealth = 100;

        private static readonly float baseBaloonsSpeed = 0.8f;
        public static float BaloonsSpeed
        {
            get
            {
                if (IsSpeedSlownessBuffOn)
                    return baseBaloonsSpeed - SpeedSlownessBuffModifier;
                else
                    return baseBaloonsSpeed;
            }
        }

        public static readonly float BallonsGenerationFrequensy = 1;
        public static readonly float PlanesGenerationFrequensy = 1f;

        public static bool IsMoneyIncreaseBuffOn = false;
        public static readonly int MoneyIncreaseBuffMultiplayer = 4;
        public static readonly float MoneyIncreaseBuffProbability = 0.05f;

        public static bool IsSpeedSlownessBuffOn = false;
        public static readonly float SpeedSlownessBuffModifier = 0.5f;
        public static readonly float SpeedSlownessBuffProbability = 0.05f;

        // Settings Screen configurations
        public static bool isTutotrialOn = true;

    }
}