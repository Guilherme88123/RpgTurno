using Domain.Model.Entity.Units.Base;

namespace RpgTurno.Screen.Play.Attack;

public class AttackManager
{
    public void ExecuteAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        target.Health -= sender.Damage;
    }
}
