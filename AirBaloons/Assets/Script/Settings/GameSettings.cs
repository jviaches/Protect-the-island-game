using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Settings
{
    public static class GameSettings
    {
        public static float BornRadius = 40;    //point on the surface of a sphere with radius 40

        public static Vector3 PlanesBornPosition = new Vector3(-160, 7.2f, -87f);

        public static float BallonsGenerationFrequensy = 2.5f;
        public static float PlanesGenerationFrequensy = 1f;

        public static bool IsMoneyIncreaseBuffOn = false;
        public static int MoneyIncreaseBuffMultiplayer = 4;
        public static float MoneyIncreaseBuffProbability = 0.8f;
    }
}

