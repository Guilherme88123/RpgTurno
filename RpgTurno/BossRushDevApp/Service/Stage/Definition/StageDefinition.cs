using Domain.Enum.Region;
using Domain.Enum.Stage;
using Domain.Enum.Unit;
using System.Collections.Generic;

namespace RpgTurno.Service.Map.Stage.Definition;

public record StageDefinition
{
    public required StageCode StageCode { get; init; }
    public required RegionCode RegionCode { get; init; }

    public required int WaveCount { get; init; }
    public required int BaseWaveBudget { get; init; }
    public required int WaveBudgetIncrease { get; init; }

    public bool IsBossStage { get; init; }

    public UnitCode? BossCode { get; init; }
    public int BossLevel { get; set; }

    public IReadOnlyList<UnitCode> BossSupportUnits { get; init; } = [];
}
