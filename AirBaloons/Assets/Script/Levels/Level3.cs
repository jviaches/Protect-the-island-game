using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Levels
{
    public class Level3 : ILevel
    {
        public int LevelIndex { get { return 3; } }

        public float BaloonGenerationFrequencyModifier { get { return 1.3f; } }

        public float PlaneGenerationFrequencyModifier { get { return 1.3f; } }

        public int MoneyGenerationModifier { get { return 1; } }
    }
}
