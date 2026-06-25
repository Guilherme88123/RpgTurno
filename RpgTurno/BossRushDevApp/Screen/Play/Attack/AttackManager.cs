using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Attack;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using RpgTurno.Screen.Play.Delay;
using System;

namespace RpgTurno.Screen.Play.Attack;

public class AttackManager
{
    public AttackPhase CurrentPhase { get; private set; } = AttackPhase.Idle;

    private BaseUnitEntity _sender;
    private BaseUnitEntity _target;
    private Vector2 _senderOriginPosition;
    private Vector2 _targetPosition;

    private float _moveSpeed = 500f;
    private int _walkFrontDistance = 200;
    private int _targetDistance = 150;

    private readonly DelayManager _delayManager = new();

    public void StartAttack(BaseUnitEntity sender, BaseUnitEntity target, bool isEnemy)
    {
        _sender = sender;
        _target = target;
        _senderOriginPosition = new Vector2(sender.PositionX, sender.PositionY);
        _targetPosition = sender.IsRanged 
            ? new Vector2(sender.Center.X + (isEnemy ? -_walkFrontDistance : _walkFrontDistance), sender.Center.Y) 
            : new Vector2(target.Center.X + (isEnemy ? _targetDistance : -_targetDistance), target.Center.Y);

        CurrentPhase = AttackPhase.MovingToTarget;
        sender.CreatureState =  CreatureStateType.Running;
    }

    public (BaseUnitEntity, BaseUnitEntity) ExecuteAttack()
    {
        _target.TakeDamage(_sender.Damage);

        CurrentPhase = AttackPhase.MovingBack;
        _sender.CreatureState = CreatureStateType.Running;

        return (_sender, _target);
    }

    public void Update()
    {
        _delayManager.Update();
    }

    public void UpdateMovingToTarget()
    {
        var senderPos = new Vector2(_sender.Center.X, _sender.Center.Y);
        var direction = _targetPosition - senderPos;

        if (direction.Length() < 10f)
        {
            CurrentPhase = AttackPhase.Attacking;
            _sender.CreatureState = CreatureStateType.Attacking;
            _delayManager.ResetDelayAttackExecution();
            return;
        }

        direction.Normalize();
        _sender.PositionX += direction.X * _moveSpeed * GlobalVariablesDto.DeltaTime;
        _sender.PositionY += direction.Y * _moveSpeed * GlobalVariablesDto.DeltaTime;
    }

    public bool HasAttackFinished()
    {
        return _delayManager.HasDelayAttackExecutionComplete();
    }

    public bool HasWaitTurnFinished()
    {
        return _delayManager.HasDelayTurnExecutionComplete();
    }

    public void UpdateMovingBack()
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
            _delayManager.ResetDelayTurnExecution();
            return;
        }

        direction.Normalize();
        _sender.PositionX += direction.X * _moveSpeed * GlobalVariablesDto.DeltaTime;
        _sender.PositionY += direction.Y * _moveSpeed * GlobalVariablesDto.DeltaTime;
    }

    // Chamado quando a animação de ataque termina (via delay)
    public (BaseUnitEntity sender, BaseUnitEntity target) ResolveAttack()
    {
        if (_sender.IsRanged)
        {
            CurrentPhase = AttackPhase.WaitingTurn;
        }
        else
        {
            CurrentPhase = AttackPhase.MovingBack;
            _sender.CreatureState = CreatureStateType.Running;
        }

        return (_sender, _target);
    }

    public void Reset()
    {
        CurrentPhase = AttackPhase.Idle;
        _sender = null;
        _target = null;
    }
}