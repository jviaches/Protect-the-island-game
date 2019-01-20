using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Levels
{
    public class Level2 : ILevel
    {
        public int LevelIndex { get { return 2; } }

        public float BaloonGenerationFrequencyModifier { get { return 0.95f; } }

        public float PlaneGenerationFrequencyModifier { get { return 40f; } }

        public int MoneyGenerationModifier { get { return 1; } }
    }
}
