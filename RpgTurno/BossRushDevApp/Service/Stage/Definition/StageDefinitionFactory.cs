using Domain.Enum.Stage;
using System;

namespace RpgTurno.Service.Map.Stage.Definition;

public class StageDefinitionFactory
{
    public static StageDefinition Create(StageCode stageCode)
    {
        return stageCode switch
        {
            StageCode.EvilTower => StageDefinitions.EvilTower,
            StageCode.BarracksOfValor => StageDefinitions.BarracksOfValor,
            StageCode.TheCastle => StageDefinitions.TheCastle,

            _ => throw new ArgumentOutOfRangeException($"Stage '{stageCode}' not has a definition configured yet!")
        };
    }
}
