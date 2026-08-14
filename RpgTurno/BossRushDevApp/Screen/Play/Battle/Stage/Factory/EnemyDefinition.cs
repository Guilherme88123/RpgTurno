using Domain.Application.Entity.Units.Base;
using Domain.Enum.Unit;
using Service.Unit;
using System;

namespace RpgTurno.Screen.Play.Battle.Stage.Factory;

public class EnemyDefinition
{
    public required UnitCode UnitCode { get; init; }
    public required int WaveCost { get; init; }
    public required int SpawnWeight { get; init; }
    public required int MaxCopies { get; init; }
    public Func<GenerateWaveContext, bool> CanSpawn { get; init; } = _ => true;

    public Func<BaseUnitEntity> Create => () => UnitFactory.Create(UnitCode);
}
