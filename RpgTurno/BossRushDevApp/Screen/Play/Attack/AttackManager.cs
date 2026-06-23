using Domain.Model.Entity.Units.Base;
using System;

namespace RpgTurno.Screen.Play.Attack;

public class AttackManager
{
    private BaseUnitEntity _sender;
    private BaseUnitEntity _target;

    public bool HasPendingAttack()
    {
        return _sender is not null && _target is not null;
    }

    public (BaseUnitEntity, BaseUnitEntity) ExecuteAttack()
    {
        if (!HasPendingAttack())
            throw new Exception("Invalid attack executed");

        _target.Health -= _sender.Damage;

        var sender = _sender;
        var target = _target;

        _sender = null;
        _target = null;

        return (sender, target);
    }

    public void StartAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        _sender = sender;
        _target = target;
    }
}
