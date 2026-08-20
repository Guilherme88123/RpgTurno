using Domain.Application.Entity.Units.Base;
using Domain.Enum.Unit;
using RpgTurno.Service.Map.Region;
using RpgTurno.Service.Map.Stage.Data;
using RpgTurno.Service.Map.Stage.Definition;
using RpgTurno.Service.Region.Level;
using Service.Unit;
using System;
using System.Collections.Generic;

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
                waves.Add(CreateBossWave(regionDefinition, stageDefinition));
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

    private static WaveData CreateBossWave(RegionDefinition regionDefinition, StageDefinition stageDefinition)
    {
        if (stageDefinition.BossCode is null)
            throw new InvalidOperationException($"Boss stage {stageDefinition.StageCode} has no boss.");

        var boss = UnitFactory.Create(stageDefinition.BossCode.Value, level: stageDefinition.BossLevel);

        var enemies = GetBossSupportUnits(regionDefinition, stageDefinition.BossSupportUnits);

        enemies.Insert(1, boss);

        return new WaveData(enemies, boss);
    }

    private static List<BaseUnitEntity> GetBossSupportUnits(
        RegionDefinition regionDefinition, 
        IReadOnlyCollection<UnitCode> supportUnitCodes)
    {
        List<BaseUnitEntity> supportUnits = new();

        foreach (var unitCode in supportUnitCodes)
        {
            var unitLevel = LevelRangeGenerator.Generate(regionDefinition.EnemyLevelRange);
            supportUnits.Add(UnitFactory.Create(unitCode, unitLevel));
        }

        return supportUnits;
    }
}
