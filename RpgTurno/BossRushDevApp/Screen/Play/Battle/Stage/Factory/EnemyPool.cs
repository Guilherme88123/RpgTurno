using Domain.Enum.Unit;
using System.Collections.Generic;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public static class EnemyPool
{
    public static List<EnemyDefinition> Available =
    [
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilWarrior,
            WaveCost = 3,
            SpawnWeight = 40,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilArcher,
            WaveCost = 2,
            SpawnWeight = 60,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilLancer,
            WaveCost = 4,
            SpawnWeight = 25,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilCleric,
            WaveCost = 4,
            SpawnWeight = 20,
            MaxCopies = 1,
        },
        new EnemyDefinition()
        {
            UnitCode = UnitCode.EvilPawn,
            WaveCost = 4,
            SpawnWeight = 22,
            MaxCopies = 2,
        },
    ];
}
