using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Attack;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using RpgTurno.Screen.Play.Battle.Delay;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace RpgTurno.Screen.Play.Battle.Attack;

public class AttackManager
{
    public AttackPhase CurrentPhase { get; private set; }

    private BaseUnitEntity _sender;
    private BaseUnitEntity _target;

    private Vector2 _senderOriginPosition;
    private Vector2 _targetPosition;

    private const float _moveSpeed = 500f;
    private const int _walkFrontDistance = 200;
    private const int _targetDistance = 150;

    private readonly DelayManager _delayManager = new();

    public Action<BaseUnitEntity, BaseUnitEntity, int> OnExecuteAttack { get; set; }
    public Action<BaseUnitEntity, BaseUnitEntity> OnTurnFinish { get; set; }
    public Action<BaseUnitEntity> OnUnitSlay { get; set; }

    public bool IsExecuting()
    {
        return CurrentPhase != AttackPhase.Idle;
    }

    public void StartAttack(BaseUnitEntity sender, BaseUnitEntity target, bool isEnemy)
    {
        _sender = sender;
        _target = target;
        _senderOriginPosition = new Vector2(sender.PositionX, sender.PositionY);
        _targetPosition = sender.IsRanged
            ? new Vector2(sender.Center.X + (isEnemy ? -_walkFrontDistance : _walkFrontDistance), sender.Center.Y)
            : new Vector2(target.Center.X + (isEnemy ? _targetDistance : -_targetDistance), target.Center.Y);

        CurrentPhase = AttackPhase.MovingToTarget;
        sender.CreatureState = CreatureStateType.Running;
    }

    public void ExecuteAttack()
    {
        var damage = _target.RecieveAttack(_sender);

        if (_target.Stats.HasHealthFinished())
            OnUnitSlay?.Invoke(_target);

        CurrentPhase = AttackPhase.MovingBack;
        _sender.CreatureState = CreatureStateType.Running;

        OnExecuteAttack?.Invoke(_sender, _target, damage);
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

        OnTurnFinish?.Invoke(_sender, _target);

        GoToIdlePhase();
    }

    public void GoToIdlePhase()
    {
        CurrentPhase = AttackPhase.Idle;
        _sender = null;
        _target = null;
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