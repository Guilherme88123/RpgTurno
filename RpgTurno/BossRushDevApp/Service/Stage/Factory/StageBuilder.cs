using RpgTurno.Service.Map.Region;
using RpgTurno.Service.Map.Stage.Data;
using RpgTurno.Service.Map.Stage.Definition;
using Service.Unit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Service.Map.Stage.Factory;

public static class StageBuilder
{
    public static StageData Build(StageDefinition stageDefinition, RegionDefinition regionDefinition)
    {
        var waveGenerator = new WaveGenerator();

        var waves = new List<WaveData>();

        for (int i = 0; i < stageDefinition.WaveCount; i++)
        {
            if (stageDefinition.IsBossStage && i == stageDefinition.WaveCount - 1)
            {
                waves.Add(CreateBossWave(stageDefinition));
                break;
            }

            waves.Add(CreateNormalWave(stageDefinition, regionDefinition, i, waveGenerator));
        }

        return new StageData(waves);
    }

    private static WaveData CreateNormalWave(
        StageDefinition stageDefinition, 
        RegionDefinition regionDefinition, 
        int currentWaveIndex, 
        WaveGenerator waveGenerator)
    {
        var budget = stageDefinition.BaseWaveBudget + currentWaveIndex * stageDefinition.WaveBudgetIncrease;

        return waveGenerator.Generate(regionDefinition, currentWaveIndex + 1, budget);
    }

    private static WaveData CreateBossWave(StageDefinition stageDefinition)
    {
        if (stageDefinition.BossCode is null)
            throw new InvalidOperationException($"Boss stage {stageDefinition.StageCode} has no boss.");

        var boss = UnitFactory.Create(stageDefinition.BossCode.Value);

        var enemies = stageDefinition.BossSupportUnits
            .Select(x => UnitFactory.Create(x, level: stageDefinition.BossLevel))
            .ToList();

        enemies.Insert(1, boss);

        return new WaveData(enemies, boss);
    }
}
