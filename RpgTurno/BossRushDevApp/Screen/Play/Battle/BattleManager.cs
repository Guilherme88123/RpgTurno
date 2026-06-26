using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Screen.Play.Battle.Attack;
using RpgTurno.Screen.Play.Battle.Stage;
using RpgTurno.Screen.Play.Battle.Turn;
using System;
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

    public Action<BaseUnitEntity, BaseUnitEntity> OnExecuteAttack { get; set; }

    #region Initialize

    public void Initialize(List<BaseUnitEntity> allies)
    {
        Allies = allies;

        _stage = StageFactory.Create();

        _attackManager.OnExecuteAttack += ExecuteAttack;
        _attackManager.OnTurnFinish += OnTurnFinish;

        InitializeUnits();
    }

    private void InitializeUnits()
    {
        _turnManager.SetUnitsQueue(GetAllUnits());
        SetUnitsPosition();
    }

    public List<BaseUnitEntity> GetAllUnits()
    {
        return [.. Allies, .. Enemies];
    }

    public List<BaseUnitEntity> GetUnitsTurnQueue()
    {
        return _turnManager.GetUnitQueueList();
    }

    public BaseUnitEntity GetCurrentUnitTurnQueue()
    {
        return _turnManager.GetPeekUnit();
    }

    #endregion

    #region Update

    public void Update(GameTime gameTime)
    {
        _attackManager.Update();

        UpdateUnits();
        UpdateTurn();
    }

    private void UpdateUnits()
    {
        foreach (var unit in GetAllUnits())
        {
            unit.Update();
        }
    }

    private void UpdateTurn()
    {
        if (_attackManager.IsExecuting())
            return;

        UpdateTurnAction();
    }

    private void OnTurnFinish(BaseUnitEntity sender, BaseUnitEntity target)
    {
        GoToNextTurn();
        VerifyWave();
    }

    private void GoToNextTurn()
    {
        _turnManager.NextTurn();
    }

    private void UpdateTurnAction()
    {
        var currentUnit = _turnManager.GetPeekUnit();

        if (IsEnemyUnit(currentUnit))
            UpdateEnemyTurn(currentUnit);
        else
            UpdateAllyTurn(currentUnit);
    }

    private bool IsEnemyUnit(BaseUnitEntity unit)
    {
        return Enemies.Contains(unit);
    }

    private void UpdateEnemyTurn(BaseUnitEntity enemyUnit)
    {
        if (!Allies.Any())
            return;

        var targetAlly = Allies.Shuffle().First();

        StartAttack(enemyUnit, targetAlly);
    }

    private void UpdateAllyTurn(BaseUnitEntity allyUnit)
    {
        if (GlobalVariablesDto.PreviousMouseDown)
            return;

        if (GlobalVariablesDto.MouseState.LeftButton != ButtonState.Pressed)
            return;

        if (!HasCursorHoveringEntity())
            return;

        var targetEnemy = GetCursorHoveringEntity();

        if (Allies.Contains(targetEnemy))
            return;

        StartAttack(allyUnit, targetEnemy);
    }

    private void StartAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        _attackManager.StartAttack(sender, target, IsEnemyUnit(sender));
    }

    private void ExecuteAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        OnExecuteAttack?.Invoke(sender, target);

        if (target.IsDestroyed)
            RemoveUnit(target);
    }

    public void RemoveUnit(BaseUnitEntity unit)
    {
        Allies.Remove(unit);
        Enemies.Remove(unit);

        _turnManager.RemoveUnit(unit);
    }

    public void VerifyWave()
    {
        if (Enemies.Any())
            return;

        AdvanceWave();
    }

    public void AdvanceWave()
    {
        if (!_stage.HasNextWave())
            return;

        _stage.NextWave();
        InitializeUnits();
    }

    public bool HasFinishedBattle()
    {
        return _stage.IsFinished();
    }

    #endregion

    #region Hover Units

    public bool HasCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return GetAllUnits().Any(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    public BaseUnitEntity GetCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return GetAllUnits().First(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    #endregion

    #region Set Units Position

    private void SetUnitsPosition()
    {
        SetAlliesPosition();
        SetEnemiesPosition();
    }

    private void SetAlliesPosition()
    {
        int posX = GlobalOptionsDto.WidthSize / 3;

        Allies.ForEach(x => x.PositionX = posX);

        SetUnitsYPosition(Allies);
        FixUnitsPositionBySize(Allies);
    }

    private void SetEnemiesPosition()
    {
        int posX = (GlobalOptionsDto.WidthSize / 3) * 2;

        Enemies.ForEach(x => x.PositionX = posX);
        Enemies.ForEach(x => x.Direction = DirectionType.Left);

        SetUnitsYPosition(Enemies);
        FixUnitsPositionBySize(Enemies);
    }

    private void SetUnitsYPosition(List<BaseUnitEntity> entitiesList)
    {
        if (!entitiesList.Any()) return;

        int entityHeight = 150;
        int fixedTopMargin = 50;
        int margin = 30;
        int step = entityHeight + margin;
        int totalHeight = entitiesList.Count * entityHeight + (entitiesList.Count - 1) * margin;
        int initialY = GlobalOptionsDto.HeightSize / 2 - totalHeight / 2 + fixedTopMargin;

        foreach (var (entity, index) in entitiesList.Select((e, i) => (e, i)))
        {
            entity.PositionY = initialY + step * index;
        }
    }

    private void FixUnitsPositionBySize(List<BaseUnitEntity> entitiesList)
    {
        if (!entitiesList.Any()) return;

        foreach (var entity in entitiesList)
        {
            entity.PositionX -= entity.SizeX / 2;
            entity.PositionY -= entity.SizeY / 2;
        }
    }

    #endregion
}
