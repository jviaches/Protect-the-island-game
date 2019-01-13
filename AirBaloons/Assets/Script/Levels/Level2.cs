using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Levels
{
    public class Level2 : ILevel
    {
        public int LevelIndex { get  { return 2; } }

        public float LevelTimer { get { return 30f; } }
    }
}
