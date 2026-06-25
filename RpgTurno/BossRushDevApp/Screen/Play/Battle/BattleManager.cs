using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using RpgTurno.Screen.Play.Attack;
using RpgTurno.Screen.Play.Battle.Stage;
using RpgTurno.Screen.Play.Turn;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play.Battle;

public class BattleManager
{
    private readonly TurnQueueManager _turnManager = new();
    private readonly AttackManager _attackManager = new();

    private StageData _stage;

    public List<BaseUnitEntity> Allies { get; private set; }
    public List<BaseUnitEntity> Enemies => _stage.GetCurrentWave().Enemies;

    public void Initialize(List<BaseUnitEntity> allies)
    {
        Allies = allies;

        _stage = StageFactory.Create();
    }

    public List<BaseUnitEntity> GetAllUnits()
    {
        return [.. Allies, .. Enemies];
    }

    public void Update(GameTime gameTime)
    {
        UpdateUnits();
        _attackManager.Update();
    }

    public void UpdateUnits()
    {
        foreach (var unit in GetAllUnits())
        {
            unit.Update();
        }
    }

    public void RemoveUnit(BaseUnitEntity unit)
    {
        Allies.Remove(unit);
        Enemies.Remove(unit);

        VerifyWave();
    }

    public void VerifyWave()
    {
        if (!Enemies.Any())
        {
            AdvanceWave();
        }
    }

    public void AdvanceWave()
    {
        if (_stage.HasNextWave())
        {
            _stage.NextWave();
        }
    }

    public bool HasFinishedBattle()
    {
        return _stage.IsFinished();
    }
}
