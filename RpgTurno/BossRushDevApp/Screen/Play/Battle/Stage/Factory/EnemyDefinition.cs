using Domain.Model.Entity.Units.Base;
using System;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public class EnemyDefinition
{
    public required Func<BaseUnitEntity> Create { get; init; }
    public required int WaveCost { get; init; }
    public required int SpawnWeight { get; init; }
    public required int MaxCopies { get; init; }
    public Func<GenerateWaveContext, bool> CanSpawn { get; init; } = _ => true;
}
