using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Battle;
using Domain.Enum.Skill.Target;
using Domain.Enum.Stage;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Unit;
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

public class BattleManager
{
    private readonly TurnQueueManager _turnManager = new();
    private readonly AttackManager _attackManager = new();

    private StageData _stage;

    public List<BaseUnitEntity> Allies { get; private set; }
    public List<BaseUnitEntity> Enemies => _stage.GetCurrentWave().Enemies;

    private List<BaseUnitEntity> _deadUnits = new();

    public Action<BaseUnitEntity, List<BaseUnitEntity>, UnitSkill, int> OnSkillExecute { get; set; }
    public Action<BaseUnitEntity, BaseUnitEntity> OnTurnFinish { get; set; }
    public Action<BaseUnitEntity, bool> OnTurnStart { get; set; }
    public Action<bool> OnBattleFinish { get; set; }

    private UnitSkill _selectedSkill;
    private bool HasAttacked;

    public BattleState BattleState { get; set; }
    public bool CanSelectSkill => BattleState == BattleState.WaitingSkillSelect || BattleState == BattleState.WaitingTargetSelect;

    private readonly float _waveTransitionSpeed = 400f;

    #region Initialize

    public void Initialize(List<BaseUnitEntity> allies, StageCode stageCode)
    {
        Allies = allies;

        _stage = StageFactory.Create(stageCode);

        _attackManager.OnExecuteSkill += ExecuteAttack;
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
        if (!CanTurnContinue())
            return;

        switch (BattleState)
        {
            case BattleState.WaveTransition:
                UpdateWaveTransition();
                break;

            case BattleState.WaitingSkillSelect:
                UpdateSkillSelect();
                break;

            case BattleState.Fighting:
            case BattleState.WaitingTargetSelect:
                UpdateTurnAction();
                break;
        }
    }

    private bool CanTurnContinue()
    {
        var isAttacking = _attackManager.IsExecuting();

        if (isAttacking)
        {
            HasAttacked = true;
            return false;
        }

        if (!isAttacking && HasAttacked && BattleState != BattleState.WaveTransition)
        {
            HasAttacked = false;
            StartTurn();
        }

        return true;
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

        StartTurn();
    }

    private void StartTurn()
    {
        BattleState = BattleState.WaitingSkillSelect;

        var unitTurn = _turnManager.GetPeekUnit();
        OnTurnStart?.Invoke(unitTurn, IsEnemyUnit(unitTurn));
        TickSkills(unitTurn);
    }

    private void TickSkills(BaseUnitEntity unit)
    {
        unit.TickSkills();
    }

    private void UpdateSkillSelect()
    {
        var currentUnit = _turnManager.GetPeekUnit();

        if (IsEnemyUnit(currentUnit))
            EnemySkillSelect(currentUnit);
        else
            AllySkillSelect(currentUnit);
    }

    private void EnemySkillSelect(BaseUnitEntity enemy)
    {
        //TODO: Adicionar Heurística para decisão de Skill
        var skill = enemy.Skills.Shuffle().FirstOrDefault(x => x.CanUse());

        if (skill is null)
            return;

        _selectedSkill = skill;

        BattleState = BattleState.Fighting;
    }

    private void AllySkillSelect(BaseUnitEntity ally)
    {
        if (_selectedSkill is null)
            return;

        BattleState = BattleState.WaitingTargetSelect;
    }

    public void SetPlayerSelectedSkill(UnitSkill skill)
    {
        if (!skill.CanUse())
            return;

        _selectedSkill = skill;
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

        var avaliable = GetAvaliableTargets(enemyUnit);

        //TODO: Adicionar Heurística para seleção de target
        var allySelected = avaliable.Shuffle().First();

        var target = GetTargetsBySelectedUnit(allySelected, avaliable);

        StartAttack(enemyUnit, target);
    }

    private void UpdateAllyTurn(BaseUnitEntity allyUnit)
    {
        BattleState = BattleState.WaitingTargetSelect;

        if (_selectedSkill is null)
            return;

        if (GlobalVariablesDto.PreviousMouseDown)
            return;

        if (GlobalVariablesDto.MouseState.LeftButton != ButtonState.Pressed)
            return;

        if (!HasCursorHoveringEntity())
            return;

        var enemySelected = GetCursorHoveringEntity();

        var avaliable = GetAvaliableTargets(allyUnit);

        if (!avaliable.Contains(enemySelected))
            return;

        var target = GetTargetsBySelectedUnit(enemySelected, avaliable);

        BattleState = BattleState.Fighting;
        StartAttack(allyUnit, target);
    }

    private void StartAttack(BaseUnitEntity sender, List<BaseUnitEntity> targets)
    {
        _attackManager.StartAttack(new SkillExecuteData(sender, targets), _selectedSkill, IsEnemyUnit(sender));
    }

    private void ExecuteAttack(BaseUnitEntity sender, List<BaseUnitEntity> targets, UnitSkill skill, int damage)
    {
        OnSkillExecute?.Invoke(sender, targets, skill, damage);

        _selectedSkill = null;

        MoveUnitToDeadList(targets.Where(x => x.IsDead).ToList());

        if (targets.Any(x => x.IsDead))
            VerifyPlayFinish();
    }

    private void MoveUnitToDeadList(List<BaseUnitEntity> units)
    {
        foreach (var unit in units)
            MoveUnitToDeadList(unit);
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
        Allies.ForEach(x => x.Direction = DirectionType.Right);
    }

    #endregion

    #region Avaliable Targets

    private List<BaseUnitEntity> GetAvaliableTargets(BaseUnitEntity sender)
    {
        return _selectedSkill.TargetType switch
        {
            TargetSkillType.Self => [sender],
            TargetSkillType.Ally => IsEnemyUnit(sender) ? Enemies : Allies,
            TargetSkillType.Enemy => IsEnemyUnit(sender) ? Allies : Enemies,
            TargetSkillType.Any => GetAllUnits(),
        };
    }

    private List<BaseUnitEntity> GetTargetsBySelectedUnit(BaseUnitEntity selectedUnit, List<BaseUnitEntity> avaliable)
    {
        return _selectedSkill.TargetAmount switch
        {
            TargetSkillAmount.Single => [selectedUnit],
            TargetSkillAmount.All => avaliable,
        };
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
