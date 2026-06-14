using Domain.Enum;
using Domain.Model.Entity.Base;

namespace Domain.Model.Entity.Units.Base;

public class BaseUnitEntity : BaseEntity
{
    public int MaxHealth { get; set; } = 10;
    public int Health { get; set; }

    public int Damage { get; set; } = 4;

    public BaseUnitEntity()
    {
        Health = MaxHealth;
    }

    public override void Update()
    {
        base.Update();

        CreatureState = CreatureStateType.Idle;
    }
}
