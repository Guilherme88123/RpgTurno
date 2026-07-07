using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Attack;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Unit;
using Microsoft.Xna.Framework;
using RpgTurno.Screen.Play.Battle.Delay;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RpgTurno.Screen.Play.Battle.Attack;

public class AttackManager
{
    public AttackPhase CurrentPhase { get; private set; }

    private SkillExecuteData _executeData;
    private BaseUnitEntity _sender => _executeData.Sender;
    private BaseUnitEntity _principalTarget => _executeData.Target;
    private UnitSkill _skill;

    private Vector2 _senderOriginPosition;
    private Vector2 _targetPosition;

    private const float _moveSpeed = 500f;
    private const int _walkFrontDistance = 200;
    private const int _targetDistance = 150;

    private readonly DelayManager _delayManager = new();

    public Action<BaseUnitEntity, List<BaseUnitEntity>, int> OnExecuteSkill { get; set; }
    public Action<BaseUnitEntity, BaseUnitEntity> OnTurnFinish { get; set; }
    public Action<BaseUnitEntity> OnUnitSlay { get; set; }

    public bool IsExecuting()
    {
        return CurrentPhase != AttackPhase.Idle;
    }

    public void StartAttack(SkillExecuteData data, UnitSkill skill, bool isEnemy)
    {
        _executeData = data;
        _skill = skill;

        _senderOriginPosition = new Vector2(_sender.PositionX, _sender.PositionY);
        _targetPosition = skill.Animation.IsRanged
            ? new Vector2(_sender.Center.X + (isEnemy ? -_walkFrontDistance : _walkFrontDistance), _sender.Center.Y)
            : new Vector2(_principalTarget.Center.X + (isEnemy ? _targetDistance : -_targetDistance), _principalTarget.Center.Y);

        CurrentPhase = AttackPhase.MovingToTarget;
        _sender.CreatureState = CreatureStateType.Running;
    }

    public void ExecuteAttack()
    {
        var result = _skill.ExecuteSkill(_executeData);

        VerifyDeadUnits();

        CurrentPhase = AttackPhase.MovingBack;
        _sender.CreatureState = CreatureStateType.Running;

        OnExecuteSkill?.Invoke(_sender, _executeData.Targets, result.Value);
    }

    private void VerifyDeadUnits()
    {
        foreach (var target in _executeData.Targets)
        {
            if (target.Stats.IsDead)
                OnUnitSlay?.Invoke(_principalTarget);
        }
    }

    public void Update()
    {
        _delayManager.Update();

        switch (CurrentPhase)
        {
            case AttackPhase.MovingToTarget:
                UpdateMovingToTarget();
                break;
            case AttackPhase.Attacking:
                UpdateAttacking();
                break;
            case AttackPhase.MovingBack:
                UpdateMovingBack();
                break;
            case AttackPhase.WaitingTurn:
                UpdateWaitingTurn();
                break;
        }
    }

    private void UpdateMovingToTarget()
    {
        var senderPos = new Vector2(_sender.Center.X, _sender.Center.Y);
        var direction = _targetPosition - senderPos;

        if (direction.Length() < 10f)
        {
            CurrentPhase = AttackPhase.Attacking;
            _sender.CreatureState = CreatureStateType.Attacking;
            ResetDelayAttack();
            return;
        }

        direction.Normalize();
        _sender.PositionX += direction.X * _moveSpeed * GlobalVariablesDto.DeltaTime;
        _sender.PositionY += direction.Y * _moveSpeed * GlobalVariablesDto.DeltaTime;
    }

    private void UpdateAttacking()
    {
        if (!HasAttackDelayFinished())
            return;

        ExecuteAttack();

        CurrentPhase = AttackPhase.MovingBack;
    }

    private void UpdateMovingBack()
    {
        var originPos = _senderOriginPosition;
        var senderPos = new Vector2(_sender.PositionX, _sender.PositionY);
        var direction = originPos - senderPos;

        if (direction.Length() < 10f)
        {
            _sender.PositionX = originPos.X;
            _sender.PositionY = originPos.Y;
            CurrentPhase = AttackPhase.WaitingTurn;
            _sender.CreatureState = CreatureStateType.Idle;
            ResetDelayTurn();
            return;
        }

        direction.Normalize();
        _sender.PositionX += direction.X * _moveSpeed * GlobalVariablesDto.DeltaTime;
        _sender.PositionY += direction.Y * _moveSpeed * GlobalVariablesDto.DeltaTime;
    }

    private void UpdateWaitingTurn()
    {
        if (!HasTurnDelayFinished())
            return;

        OnTurnFinish?.Invoke(_sender, _principalTarget);

        GoToIdlePhase();
    }

    public void GoToIdlePhase()
    {
        CurrentPhase = AttackPhase.Idle;
    }

    #region Delay Manager

    public void ResetDelayAttack()
    {
        _delayManager.ResetDelayAttackExecution();
    }

    public bool HasAttackDelayFinished()
    {
        return _delayManager.HasDelayAttackExecutionComplete();
    }

    public void ResetDelayTurn()
    {
        _delayManager.ResetDelayAttackExecution();
    }

    public bool HasTurnDelayFinished()
    {
        return _delayManager.HasDelayAttackExecutionComplete();
    }

    #endregion
}