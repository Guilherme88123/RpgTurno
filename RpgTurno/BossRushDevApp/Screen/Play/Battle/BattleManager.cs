using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Battle;
using Domain.Enum.Skill.Target;
using Domain.Enum.Stage;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite;
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
    public List<BaseUnitEntity> AliveAllies => Allies.Where(x => !x.IsDead).ToList();

    public List<BaseUnitEntity> Enemies => _stage.GetCurrentWave().Enemies;
    public List<BaseUnitEntity> AliveEnemies => _stage.GetCurrentWave().AliveEnemies;

    public BaseUnitEntity CurrentTurnUnit { get; private set; }

    public Action<BaseUnitEntity, BaseUnitEntity> OnTurnFinish { get; set; }
    public Action<BaseUnitEntity, bool> OnTurnStart { get; set; }
    public Action<bool> OnBattleFinish { get; set; }
    public Action<UnitSkill> OnSkillSelect { get; set; }
    public Action<BaseUnitEntity> OnSlayEnemy { get; set; }

    public Action<BaseUnitEntity, AnimationClip> OnPlaySenderAnimation { get; set; }
    public Action<List<BaseUnitEntity>, AnimationClip> OnPlayTargetsAnimation { get; set; }

    public UnitSkill SelectedSkill { get; private set; }
    private bool HasAttacked;

    public BattleState BattleState { get; set; }
    public bool CanSelectSkill =>
        (BattleState == BattleState.WaitingSkillSelect || BattleState == BattleState.WaitingTargetSelect)
        && !IsEnemyUnit(CurrentTurnUnit);

    public bool IsAttacking => BattleState == BattleState.Fighting;

    private readonly float _waveTransitionSpeed = 400f;

    #region Initialize

    public void Initialize(List<BaseUnitEntity> allies, StageCode stageCode)
    {
        Allies = allies;

        _stage = StageFactory.Create(stageCode);

        _attackManager.OnExecuteSkill += ExecuteAttack;
        _attackManager.OnTurnFinish += HandleTurnFinish;
        _attackManager.OnUnitSlay += HandleEnemySlay;
        _attackManager.OnPlaySenderAnimation += PlaySenderAnimation;
        _attackManager.OnPlayTargetsAnimation += PlayTargetsAnimation;

        InitializeUnits();

        StartWaveTransition();
    }

    private void InitializeUnits()
    {
        _turnManager.SetUnitsQueue(GetLiveUnits());
        InitializeUnitsPosition();
    }

    public List<BaseUnitEntity> GetAllUnits()
    {
        return 
        [
        .. Allies, 
        .. Enemies
        ];
    }

    public List<BaseUnitEntity> GetLiveUnits()
    {
        return
        [
        .. Allies.Where(x => !x.IsDead),
        .. Enemies.Where(x => !x.IsDead)
        ];
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
        GetAllUnits().ForEach(x => x.Update());
    }

    private void UpdateTurn()
    {
        VerifyDeadUnits();

        if (!CanTurnContinue())
            return;

        VerifyWaveFinish();
        VerifyPlayFinish();

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
        AliveEnemies.ForEach(x => x.PositionX -= _waveTransitionSpeed * GlobalVariablesDto.DeltaTime);
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
        return AliveEnemies.All(x => x.PositionX <= target);
    }

    private void FinishWaveTransition(int target)
    {
        AliveAllies.ForEach(x => x.CreatureState = CreatureStateType.Idle);
        AliveEnemies.ForEach(x => x.PositionX = target);

        _turnManager.SetUnitsQueue(GetLiveUnits());

        StartTurn();
    }

    private void StartTurn()
    {
        BattleState = BattleState.WaitingSkillSelect;

        var unitTurn = _turnManager.GetPeekUnit();

        unitTurn.OnTurnStart();

        OnTurnStart?.Invoke(unitTurn, IsEnemyUnit(unitTurn));
    }

    private void UpdateSkillSelect()
    {
        CurrentTurnUnit = _turnManager.GetPeekUnit();

        if (IsEnemyUnit(CurrentTurnUnit))
            EnemySkillSelect(CurrentTurnUnit);
        else
            AllySkillSelect(CurrentTurnUnit);
    }

    private void EnemySkillSelect(BaseUnitEntity enemy)
    {
        //TODO: Adicionar Heurística para decisão de Definition
        var skill = enemy.Skills.Shuffle().FirstOrDefault(x => x.CanUse());

        if (skill is null)
            return;

        SelectedSkill = skill;
        OnSkillSelect?.Invoke(skill);

        BattleState = BattleState.Fighting;
    }

    private void AllySkillSelect(BaseUnitEntity ally)
    {
        if (SelectedSkill is null)
            return;

        BattleState = BattleState.WaitingTargetSelect;
    }

    public void SetPlayerSelectedSkill(UnitSkill skill)
    {
        if (!skill.CanUse())
            return;

        SelectedSkill = skill;
        OnSkillSelect?.Invoke(skill);
    }

    private void UpdateTurnAction()
    {
        CurrentTurnUnit = _turnManager.GetPeekUnit();

        if (IsEnemyUnit(CurrentTurnUnit))
            UpdateEnemyTurn(CurrentTurnUnit);
        else
            UpdateAllyTurn(CurrentTurnUnit);
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

        if (SelectedSkill is null)
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
        _attackManager.StartAttack(new SkillExecuteData(sender, targets), SelectedSkill, IsEnemyUnit(sender));
    }

    private void ExecuteAttack(UnitSkill skill, SkillResult result)
    {
        SelectedSkill = null;
    }

    private void VerifyDeadUnits()
    {
        var deadUnits = GetAllUnits()
            .Where(x => x.IsDead)
            .ToList();

        RemoveUnitFromTurnQueue(deadUnits);
    }

    private void RemoveUnitFromTurnQueue(List<BaseUnitEntity> units)
    {
        foreach (var unit in units)
            RemoveUnitFromTurnQueue(unit);
    }

    private void RemoveUnitFromTurnQueue(BaseUnitEntity unit)
    {
        _turnManager.RemoveUnit(unit);
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
        return AliveAllies.Count == 0;
    }

    private void BattleFinish(bool isGameOver = false)
    {
        BattleState = BattleState.Finished;
        OnBattleFinish?.Invoke(isGameOver);
    }

    private void HandleTurnFinish(BaseUnitEntity sender, BaseUnitEntity target)
    {
        OnTurnFinish?.Invoke(sender, target);
        GoToNextTurn();

        VerifyWaveFinish();
    }

    private void HandleEnemySlay(BaseUnitEntity unit)
    {
        if (!IsEnemyUnit(unit))
            return;

        Allies.ForEach(x => x.Stats.AddExperience(unit.Stats));
        OnSlayEnemy?.Invoke(unit);
    }

    private void GoToNextTurn()
    {
        _turnManager.NextTurn();
    }

    private void VerifyWaveFinish()
    {
        if (AliveEnemies.Count >= 1)
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
        AliveAllies.ForEach(x => x.CreatureState = CreatureStateType.Running);
        AliveAllies.ForEach(x => x.Direction = DirectionType.Right);
    }

    #endregion

    #region Avaliable Targets

    public List<BaseUnitEntity> GetAvaliableTargets()
    {
        return GetAvaliableTargets(CurrentTurnUnit);
    }

    private List<BaseUnitEntity> GetAvaliableTargets(BaseUnitEntity sender)
    {
        return SelectedSkill.Definition.TargetType switch
        {
            TargetSkillType.Self => [sender],
            TargetSkillType.Ally => IsEnemyUnit(sender) ? AliveEnemies : AliveAllies,
            TargetSkillType.Enemy => IsEnemyUnit(sender) ? AliveAllies : AliveEnemies,
            TargetSkillType.Any => GetLiveUnits(),
        };
    }

    public List<BaseUnitEntity> GetTargetsBySelectedUnit(BaseUnitEntity selectedUnit, List<BaseUnitEntity> avaliable)
    {
        return SelectedSkill.Definition.TargetAmount switch
        {
            TargetSkillAmount.Single => [selectedUnit],
            TargetSkillAmount.All => avaliable,
        };
    }

    #endregion

    #region Skill Animation

    private void PlaySenderAnimation(BaseUnitEntity sender, AnimationClip animation)
    {
        OnPlaySenderAnimation?.Invoke(sender, animation);
    }

    private void PlayTargetsAnimation(List<BaseUnitEntity> targets, AnimationClip animation)
    {
        OnPlayTargetsAnimation?.Invoke(targets, animation);
    }

    #endregion

    #region Hover Units

    public bool HasCursorHoveringEntity()
    {
        if (BattleState == BattleState.Finished)
            return false;

        var mouse = GlobalVariablesDto.MouseState;
        return GetLiveUnits().Any(x => x.IsHovering(mouse.Position));
    }

    public BaseUnitEntity GetCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return GetLiveUnits().First(x => x.IsHovering(mouse.Position));
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
