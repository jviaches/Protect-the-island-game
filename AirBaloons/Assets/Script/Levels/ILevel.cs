﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Levels
{
    public interface ILevel
    {
        int LevelIndex { get; }
        float BaloonGenerationFrequencyModifier { get; }
        float PlaneGenerationFrequencyModifier { get; }
        int MoneyGenerationModifier { get; }
    }
}