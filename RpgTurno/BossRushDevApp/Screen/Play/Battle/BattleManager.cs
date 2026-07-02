using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Battle;
using Domain.Enum.Stage;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Screen.Play.Battle.Attack;
using RpgTurno.Screen.Play.Battle.Stage;
using RpgTurno.Screen.Play.Battle.Stage.Factory;
using RpgTurno.Screen.Play.Battle.Turn;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play.Battle;

//TODO: Adicionar transição ao final da batalha, quando o jogador vence ou perde, para mostrar a tela de vitória ou derrota.
public class BattleManager
{
    private readonly TurnQueueManager _turnManager = new();
    private readonly AttackManager _attackManager = new();

    private StageData _stage;

    public List<BaseUnitEntity> Allies { get; private set; }
    public List<BaseUnitEntity> Enemies => _stage.GetCurrentWave().Enemies;

    private List<BaseUnitEntity> _deadUnits = new();

    public Action<BaseUnitEntity, BaseUnitEntity, int> OnExecuteAttack { get; set; }
    public Action<BaseUnitEntity, BaseUnitEntity> OnTurnFinish { get; set; }
    public Action<bool> OnBattleFinish { get; set; }

    public BattleState BattleState { get; set; }
    private readonly float _waveTransitionSpeed = 400f;

    #region Initialize

    public void Initialize(List<BaseUnitEntity> allies, StageCode stageCode)
    {
        Allies = allies;

        _stage = StageFactory.Create(stageCode);

        _attackManager.OnExecuteAttack += ExecuteAttack;
        _attackManager.OnTurnFinish += HandleTurnFinish;
        _attackManager.OnUnitSlay += HandleEnemySlay;

        InitializeUnits();

        StartWaveTransition();
    }

    private void InitializeUnits()
    {
        _turnManager.SetUnitsQueue(GetAllUnits());
        InitializeUnitsPosition();
    }

    public List<BaseUnitEntity> GetAllUnits()
    {
        return [.. Allies, .. Enemies, .. _deadUnits];
    }

    public List<BaseUnitEntity> GetUnitsTurnQueue()
    {
        return _turnManager.GetUnitQueueList();
    }

    public BaseUnitEntity GetCurrentUnitTurnQueue()
    {
        return _turnManager.GetPeekUnit();
    }

    public int GetCurrentWaveIndex()
    {
        return _stage.GetCurrentWaveIndex();
    }

    public int GetTotalCountWaves()
    {
        return _stage.GetCountWaves();
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
        UpdateLiveUnits();
        UpdateDeadUnits();
    }

    private void UpdateLiveUnits()
    {
        foreach (var unit in GetAllUnits())
        {
            unit.Update();
        }
    }

    private void UpdateDeadUnits()
    {
        List<BaseUnitEntity> destroyedUnits = new();

        foreach (var unit in _deadUnits)
        {
            unit.Update();

            if (unit.IsDestroyed)
                destroyedUnits.Add(unit);
        }

        foreach (var unit in destroyedUnits)
        {
            RemoveUnitFromDeadList(unit);
        }
    }

    private void RemoveUnitFromDeadList(BaseUnitEntity unit)
    {
        _deadUnits.Remove(unit);
    }

    private void UpdateTurn()
    {
        if (_attackManager.IsExecuting())
            return;

        if (BattleState == BattleState.WaveTransition)
        {
            UpdateWaveTransition();
            return;
        }

        UpdateTurnAction();
    }

    private void UpdateWaveTransition()
    {
        var target = GetWaveTransitionTarget();

        MoveParty();
        MoveCamera();

        if (!ReachedWaveTransitionTarget(target))
            return;

        FinishWaveTransition(target);
    }

    private int GetWaveTransitionTarget()
    {
        return GlobalOptionsDto.WidthSize / 3 * 2 - Enemies.First().SizeX / 2;
    }

    private void MoveParty()
    {
        Enemies.ForEach(x => x.PositionX -= _waveTransitionSpeed * GlobalVariablesDto.DeltaTime);
    }

    private void MoveCamera()
    {
        var cameraPosition = new Vector2();

        if (GlobalVariablesDto.SpriteBatchTransforms.TryGetValue(GlobalVariablesDto.SpriteBatchBackground, out var position))
        {
            cameraPosition = position;
        }

        GlobalVariablesDto.Follow(
            GlobalVariablesDto.SpriteBatchBackground,
            new Vector2(cameraPosition.X + _waveTransitionSpeed * GlobalVariablesDto.DeltaTime + GlobalOptionsDto.WidthSize / 2, cameraPosition.Y + GlobalOptionsDto.HeightSize / 2));

    }

    private bool ReachedWaveTransitionTarget(int target)
    {
        return Enemies.All(x => x.PositionX <= target);
    }

    private void FinishWaveTransition(int target)
    {
        Allies.ForEach(x => x.CreatureState = CreatureStateType.Idle);
        Enemies.ForEach(x => x.PositionX = target);

        _turnManager.SetUnitsQueue(GetAllUnits());

        BattleState = BattleState.Fighting;
    }

    private void UpdateTurnAction()
    {
        var currentUnit = _turnManager.GetPeekUnit();

        if (IsEnemyUnit(currentUnit))
            UpdateEnemyTurn(currentUnit);
        else
            UpdateAllyTurn(currentUnit);
    }

    public bool IsEnemyUnit(BaseUnitEntity unit)
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

    private void ExecuteAttack(BaseUnitEntity sender, BaseUnitEntity target, int damage)
    {
        OnExecuteAttack?.Invoke(sender, target, damage);

        if (target.IsDead)
        {
            MoveUnitToDeadList(target);
            VerifyPlayFinish();
        }
    }

    private void MoveUnitToDeadList(BaseUnitEntity unit)
    {
        Allies.Remove(unit);
        Enemies.Remove(unit);

        _turnManager.RemoveUnit(unit);

        _deadUnits.Add(unit);
    }

    private void VerifyPlayFinish()
    {
        VerifyStageFinish();
        VerifyGameOver();
    }

    private void VerifyStageFinish()
    {
        if (HasFinishedBattle())
            BattleFinish();
    }

    private bool HasFinishedBattle()
    {
        return _stage.IsFinished();
    }

    private void VerifyGameOver()
    {
        if (HasLostBattle())
            BattleFinish(isGameOver: true);
    }

    private bool HasLostBattle()
    {
        return !Allies.Any();
    }

    private void BattleFinish(bool isGameOver = false)
    {
        OnBattleFinish?.Invoke(isGameOver);
    }

    private void HandleTurnFinish(BaseUnitEntity sender, BaseUnitEntity target)
    {
        OnTurnFinish?.Invoke(sender, target);
        GoToNextTurn();
        VerifyWave();
    }

    private void HandleEnemySlay(BaseUnitEntity unit)
    {
        if (!IsEnemyUnit(unit))
            return;

        Allies.ForEach(x => x.Stats.AddExperience(unit.Stats));
    }

    private void GoToNextTurn()
    {
        _turnManager.NextTurn();
    }

    private void VerifyWave()
    {
        if (Enemies.Any())
            return;

        AdvanceWave();
    }

    private void AdvanceWave()
    {
        if (!_stage.HasNextWave())
            return;

        _stage.NextWave();
        SetEnemiesPosition();

        StartWaveTransition();
    }

    private void StartWaveTransition()
    {
        BattleState = BattleState.WaveTransition;
        Allies.ForEach(x => x.CreatureState = CreatureStateType.Running);
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

    private void InitializeUnitsPosition()
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
        int posX = (int)(GlobalOptionsDto.WidthSize * 1.5);
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
