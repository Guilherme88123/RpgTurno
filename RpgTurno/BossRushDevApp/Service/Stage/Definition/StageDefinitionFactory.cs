using Domain.Enum.Stage;
using System;

namespace RpgTurno.Service.Map.Stage.Definition;

public class StageDefinitionFactory
{
    public static StageDefinition Create(StageCode stageCode)
    {
        return stageCode switch
        {
            StageCode.Kingdom01 => StageDefinitions.Kingdom01,
            StageCode.Kingdom02 => StageDefinitions.Kingdom02,
            StageCode.KingdomBoss => StageDefinitions.KingdomBoss,

            _ => throw new ArgumentOutOfRangeException($"Stage '{stageCode}' not has a definition configured yet!")
        };
    }
}
