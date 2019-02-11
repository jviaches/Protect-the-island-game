using Assets.Script.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Levels
{
    public interface ILevel
    {
        int LevelIndex { get; }

        int CollectedCoins { get; set; }

        float BaloonGenerationFrequencyModifier { get; }
        float PlaneGenerationFrequencyModifier { get; }
        int MoneyGenerationModifier { get; }

        List<KeyValuePair<float,FloatItem>> TimeActivationDic { get; }
    }
}
