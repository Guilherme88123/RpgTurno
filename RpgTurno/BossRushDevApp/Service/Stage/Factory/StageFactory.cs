using Domain.Enum.Stage;
using RpgTurno.Service.Map.Region;
using RpgTurno.Service.Map.Stage.Data;
using RpgTurno.Service.Map.Stage.Definition;

namespace RpgTurno.Service.Map.Stage.Factory;

public static class StageFactory
{
    public static StageData Create(StageCode stageCode)
    {
        var stageDefinition = StageDefinitionFactory.Create(stageCode);

        var regionDefinition = RegionDefinitionFactory.Create(stageDefinition.RegionCode);

        return StageBuilder.Build(stageDefinition, regionDefinition);
    }
}
