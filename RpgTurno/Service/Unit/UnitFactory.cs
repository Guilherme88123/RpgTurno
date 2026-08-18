using Domain.Application.Entity.Units.Ally.Archer;
using Domain.Application.Entity.Units.Ally.Cleric;
using Domain.Application.Entity.Units.Ally.Lancer;
using Domain.Application.Entity.Units.Ally.Pawn;
using Domain.Application.Entity.Units.Ally.Warrior;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.Bear;
using Domain.Application.Entity.Units.Enemy.BombFish;
using Domain.Application.Entity.Units.Enemy.EvilArcher;
using Domain.Application.Entity.Units.Enemy.EvilCleric;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilPawn;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Entity.Units.Enemy.Gnoll;
using Domain.Application.Entity.Units.Enemy.Gnome;
using Domain.Application.Entity.Units.Enemy.HarpoonShark;
using Domain.Application.Entity.Units.Enemy.HexShaman;
using Domain.Application.Entity.Units.Enemy.Lizard;
using Domain.Application.Entity.Units.Enemy.Minotaur;
using Domain.Application.Entity.Units.Enemy.PaddleShark;
using Domain.Application.Entity.Units.Enemy.Panda;
using Domain.Application.Entity.Units.Enemy.PigRider;
using Domain.Application.Entity.Units.Enemy.Sheep;
using Domain.Application.Entity.Units.Enemy.Skull;
using Domain.Application.Entity.Units.Enemy.Snake;
using Domain.Application.Entity.Units.Enemy.SpearGoblin;
using Domain.Application.Entity.Units.Enemy.Spider;
using Domain.Application.Entity.Units.Enemy.SupremeWarrior;
using Domain.Application.Entity.Units.Enemy.Thief;
using Domain.Application.Entity.Units.Enemy.TorchGoblin;
using Domain.Application.Entity.Units.Enemy.Troll;
using Domain.Application.Entity.Units.Enemy.Turtle;
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
            UnitCode.Bear => new BearEntity(level),
            UnitCode.BombFish => new BombFishEntity(level),
            UnitCode.Gnoll => new GnollEntity(level),
            UnitCode.Gnome => new GnomeEntity(level),
            UnitCode.HarpoonShark => new HarpoonSharkEntity(level),
            UnitCode.HexShaman => new HexShamanEntity(level),
            UnitCode.Lizard => new LizardEntity(level),
            UnitCode.Minotaur => new MinotaurEntity(level),
            UnitCode.PaddleShark => new PaddleSharkEntity(level),
            UnitCode.Panda => new PandaEntity(level),
            UnitCode.Turtle => new TurtleEntity(level),
            UnitCode.Sheep => new SheepEntity(level),
            UnitCode.PigRider => new PigRiderEntity(level),
            UnitCode.Skull => new SkullEntity(level),
            UnitCode.Snake => new SnakeEntity(level),
            UnitCode.SpearGoblin => new SpearGoblinEntity(level),
            UnitCode.Spider => new SpiderEntity(level),
            UnitCode.Thief => new ThiefEntity(level),
            UnitCode.TorchGoblin => new TorchGoblinEntity(level),
            UnitCode.Troll => new TrollEntity(level),

            _ => throw new ArgumentOutOfRangeException(nameof(unitCode), unitCode, null)
        };
    }
}
