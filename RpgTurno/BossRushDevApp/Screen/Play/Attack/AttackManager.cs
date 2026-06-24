using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Attack;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using System;

namespace RpgTurno.Screen.Play.Attack;

public class AttackManager
{
    public AttackPhase CurrentPhase { get; private set; } = AttackPhase.Idle;

    private BaseUnitEntity _sender;
    private BaseUnitEntity _target;
    private Vector2 _senderOriginPosition;

    private float _moveSpeed = 500f;

    public bool HasPendingAttack() => CurrentPhase != AttackPhase.Idle && CurrentPhase != AttackPhase.WaitingTurn;

    public bool IsWaitingTurn() => CurrentPhase == AttackPhase.WaitingTurn;

    public void StartAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        _sender = sender;
        _target = target;
        _senderOriginPosition = new Vector2(sender.PositionX, sender.PositionY);

        CurrentPhase = sender.IsRanged ? AttackPhase.Attacking : AttackPhase.MovingToTarget;

        sender.CreatureState = sender.IsRanged ? CreatureStateType.Attacking : CreatureStateType.Running;
    }

    public (BaseUnitEntity, BaseUnitEntity) ExecuteAttack()
    {
        if (!HasPendingAttack())
            throw new Exception("Invalid attack executed");

        _target.Health -= _sender.Damage;

        return (_sender, _target);
    }

    public void Update()
    {
        switch (CurrentPhase)
        {
            case AttackPhase.MovingToTarget:
                UpdateMovingToTarget();
                break;
            case AttackPhase.MovingBack:
                UpdateMovingBack();
                break;
        }
    }

    private void UpdateMovingToTarget()
    {
        var targetPos = new Vector2(_target.PositionX, _target.PositionY);
        var senderPos = new Vector2(_sender.PositionX, _sender.PositionY);
        var direction = targetPos - senderPos;

        // chegou perto o suficiente
        if (direction.Length() < 100f)
        {
            CurrentPhase = AttackPhase.Attacking;
            _sender.CreatureState = CreatureStateType.Attacking;
            return;
        }

        direction.Normalize();
        _sender.PositionX += direction.X * _moveSpeed * GlobalVariablesDto.DeltaTime;
        _sender.PositionY += direction.Y * _moveSpeed * GlobalVariablesDto.DeltaTime;
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