using Domain.Application.Entity.Units.Base;
using RpgTurno.Service.Map.EnemyPool.Definition;
using RpgTurno.Service.Map.Region;
using RpgTurno.Service.Map.Stage.Data;
using RpgTurno.Service.Region.Level;
using Service.Unit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Service.Map.Stage.Factory;

public class WaveGenerator
{
    private readonly Random _random = new();

    public WaveData Generate(RegionDefinition regionDefinition, int waveIndex, int budget)
    {
        List<BaseUnitEntity> enemies = [];

        while (budget > 0)
        {
            var avaliable = GetAvaliable(regionDefinition, waveIndex, budget, enemies);

            if (!avaliable.Any())
                break;

            var selected = PickWeighted(avaliable);

            var enemyLevel = LevelRangeGenerator.Generate(regionDefinition.EnemyLevelRange);
            var enemy = UnitFactory.Create(selected.UnitCode, enemyLevel);

            enemies.Add(enemy);

            budget -= selected.WaveCost;
        }

        return new WaveData(enemies);
    }

    private List<EnemyDefinition> GetAvaliable(RegionDefinition regionDefinition, int wave, int budget, List<BaseUnitEntity> current)
    {
        return regionDefinition.EnemyPool.Enemies.Where(x =>
        {
            if (x.WaveCost > budget)
                return false;

            //var copies = current.Count(e => e.Uni == x.Create().GetType());

            //if (copies >= x.MaxCopies)
            //    return false;

            return x.CanSpawn(new()
                    {
                        WaveIndex = wave,
                        RemainingBudget = budget,
                        CurrentEnemyCount = current.Count
                    });
        })
        .ToList();
    }

    private EnemyDefinition PickWeighted(List<EnemyDefinition> options)
    {
        int total = options.Sum(x => x.SpawnWeight);
        int rngRoll = _random.Next(total);

        int acumulated = 0;

        foreach (var enemy in options)
        {
            acumulated += enemy.SpawnWeight;

            if (acumulated > rngRoll)
                return enemy;
        }

        return options.First();
    }
}
