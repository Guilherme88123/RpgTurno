using Domain.Enum.Stage;
using RpgTurno.Screen.Play.Battle.Stage.Factory.Stages;
using System;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public static class StageFactory
{
    public static StageData Create(StageCode stageCode)
    {
        return stageCode switch
        {
            StageCode.Tower => TowerStageFactory.Create(),
            StageCode.Barrack => BarrackStageFactory.Create(),
            StageCode.Castle => CastleStageFactory.Create(),

            _ => throw new NotImplementedException($"Stage {stageCode} not implemented.")
        };
    }
}
