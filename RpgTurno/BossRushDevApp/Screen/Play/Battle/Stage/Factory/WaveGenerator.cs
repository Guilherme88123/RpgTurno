using Domain.Model.Entity.Units.Base;
using RpgTurno.Screen.Play.Battle.Wave;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public class WaveGenerator
{
    private readonly Random _random = new();

    public WaveData Generate(int waveIndex, int budget)
    {
        List<BaseUnitEntity> enemies = [];

        while (budget > 0)
        {
            var avaliable = GetAvaliable(waveIndex, budget, enemies);

            if (!avaliable.Any())
                break;

            var selected = PickWeighted(avaliable);

            enemies.Add(selected.Create());

            budget -= selected.WaveCost;
        }

        return new WaveData(enemies);
    }

    private List<EnemyDefinition> GetAvaliable(int wave, int budget, List<BaseUnitEntity> current)
    {
        return EnemyPool.Available.Where(x =>
        {
            if (x.WaveCost > budget)
                return false;

            var copies = current.Count(e => e.GetType() == x.Create().GetType());

            if (copies >= x.MaxCopies)
                return false;

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
