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

        public static readonly Vector3 PlanesBornPosition = new Vector3(-160, 7.2f, -87f);
        public static readonly Vector3[] BalloonsBornPositions = new Vector3[]
            {
            new Vector3(-26f, 11.3f, 28f),
            new Vector3(-4.4f, 11.3f, 28f),
            new Vector3(22.2f, 5f, -9.4f),
            new Vector3(-60.8f, 7.5f, -9.4f),
            new Vector3(-19.4f, 34.3f, -9.4f)
            };


        public static int BaseIslandHealth = 100;

        private static readonly float baseBaloonsSpeed = 1f;
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
        public static readonly float MoneyIncreaseBuffProbability = 0.8f;

        public static bool IsSpeedSlownessBuffOn = false;
        public static readonly float SpeedSlownessBuffModifier = 0.5f;
        public static readonly float SpeedSlownessBuffProbability = 0.8f;

        // Settings Screen configurations
        public static bool isTutotrialOn = true;
        
    }
}