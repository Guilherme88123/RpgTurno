using Domain.Application.Entity.Units.Ally.Archer;
using Domain.Application.Entity.Units.Ally.Cleric;
using Domain.Application.Entity.Units.Ally.Lancer;
using Domain.Application.Entity.Units.Ally.Pawn;
using Domain.Application.Entity.Units.Ally.Warrior;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilArcher;
using Domain.Application.Entity.Units.Enemy.EvilCleric;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilPawn;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Entity.Units.Enemy.SupremeWarrior;
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
            UnitCode.EvilArcher => new EvilArcherEntity(level),
            UnitCode.EvilCleric => new EvilClericEntity(level),
            UnitCode.EvilLancer => new EvilLancerEntity(level),
            UnitCode.EvilWarrior => new EvilWarriorEntity(level),
            UnitCode.SupremeWarrior => new SupremeWarriorEntity(level),
            UnitCode.Pawn => new PawnEntity(level),
            UnitCode.EvilPawn => new EvilPawnEntity(level),

            _ => throw new ArgumentOutOfRangeException(nameof(unitCode), unitCode, null)
        };
    }
}
