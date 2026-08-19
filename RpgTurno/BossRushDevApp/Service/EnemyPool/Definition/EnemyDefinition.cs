using Domain.Enum.Unit;
using RpgTurno.Service.Map.Stage.Factory;
using System;

namespace RpgTurno.Service.Map.EnemyPool.Definition;

public class EnemyDefinition
{
    public required UnitCode UnitCode { get; init; }
    public required int WaveCost { get; init; }
    public required int SpawnWeight { get; init; }
    public required int MaxCopies { get; init; }
    public Func<GenerateWaveContext, bool> CanSpawn { get; init; } = _ => true;
}
