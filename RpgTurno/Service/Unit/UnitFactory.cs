using Domain.Application.Entity.Units.Ally.Archer;
using Domain.Application.Entity.Units.Ally.Cleric;
using Domain.Application.Entity.Units.Ally.Lancer;
using Domain.Application.Entity.Units.Ally.Warrior;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.Archer;
using Domain.Application.Entity.Units.Enemy.Cleric;
using Domain.Application.Entity.Units.Enemy.Lancer;
using Domain.Application.Entity.Units.Enemy.SuperWarrior;
using Domain.Application.Entity.Units.Enemy.Warrior;
using Domain.Enum.Unit;

namespace Service.Unit;

public static class UnitFactory
{
    public static BaseUnitEntity Create(UnitCode unitCode, int level = 1)
    {
        return unitCode switch
        {
            UnitCode.Archer => new ArcherEntity(level),
            UnitCode.Cleric => new ClericEntity(level),
            UnitCode.Lancer => new LancerEntity(level),
            UnitCode.Warrior => new WarriorEntity(level),
            UnitCode.EvilArcher => new EnemyArcherEntity(level),
            UnitCode.EvilCleric => new EnemyClericEntity(level),
            UnitCode.EvilLancer => new EnemyLancerEntity(level),
            UnitCode.EvilWarrior => new EnemyWarriorEntity(level),
            UnitCode.SupremeWarrior => new EnemySuperWarriorEntity(level),

            _ => throw new ArgumentOutOfRangeException(nameof(unitCode), unitCode, null)
        };
    }
}
