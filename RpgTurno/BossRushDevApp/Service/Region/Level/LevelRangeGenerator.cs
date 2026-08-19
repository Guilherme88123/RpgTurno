using System;

namespace RpgTurno.Service.Region.Level;

public static class LevelRangeGenerator
{
    public static int Generate(LevelRangeDefinition levelRange)
    {
        return Random.Shared.Next(levelRange.MinEnemyLevel, levelRange.MaxEnemyLevel + 1);
    }
}
