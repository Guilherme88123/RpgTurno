using Domain.Model.Entity.Units.Enemy.Archer;
using Domain.Model.Entity.Units.Enemy.Cleric;
using Domain.Model.Entity.Units.Enemy.Lancer;
using Domain.Model.Entity.Units.Enemy.Warrior;
using System.Collections.Generic;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public static class EnemyPool
{
    public static List<EnemyDefinition> Available =
    [
        new EnemyDefinition()
        {
            Create = () => new EnemyWarriorEntity(),
            WaveCost = 3,
            SpawnWeight = 40,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            Create = () => new EnemyArcherEntity(),
            WaveCost = 2,
            SpawnWeight = 60,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            Create = () => new EnemyLancerEntity(),
            WaveCost = 4,
            SpawnWeight = 25,
            MaxCopies = 2,
        },
        new EnemyDefinition()
        {
            Create = () => new EnemyClericEntity(),
            WaveCost = 5,
            SpawnWeight = 10,
            MaxCopies = 1,
            CanSpawn = ctx => ctx.WaveIndex >= 2,
        },
    ];
}
