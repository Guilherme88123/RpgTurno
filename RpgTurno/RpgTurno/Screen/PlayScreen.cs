using Domain.Const.Screen;
using Domain.Enum;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Archer;
using Domain.Model.Entity.Units.Cleric;
using Domain.Model.Entity.Units.Enemy.Archer;
using Domain.Model.Entity.Units.Enemy.Cleric;
using Domain.Model.Entity.Units.Enemy.Lancer;
using Domain.Model.Entity.Units.Enemy.Warrior;
using Domain.Model.Entity.Units.Lancer;
using Domain.Model.Entity.Units.Warrior;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurnoApp.Screen;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private List<BaseEntity> _entitiesList = new();

    public override void Initialize()
    {
        base.Initialize();

        var _warrior = new WarriorEntity();
        _warrior.PositionX = 300;
        _warrior.PositionY = 100;

        var _lancer = new LancerEntity();
        _lancer.PositionX = 300;
        _lancer.PositionY = 300;

        var _archer = new ArcherEntity();
        _archer.PositionX = 300;
        _archer.PositionY = 500;

        var _cleric = new ClericEntity();
        _cleric.PositionX = 300;
        _cleric.PositionY = 700;

        _entitiesList.AddRange([_warrior, _lancer, _archer, _cleric]);

        var _enemyWarrior = new EnemyWarriorEntity();
        _enemyWarrior.PositionX = 1600;
        _enemyWarrior.PositionY = 100;
        _enemyWarrior.Direction = DirectionType.Left;

        var _enemyLancer = new EnemyLancerEntity();
        _enemyLancer.PositionX = 1600;
        _enemyLancer.PositionY = 300;
        _enemyLancer.Direction = DirectionType.Left;

        var _enemyArcher = new EnemyArcherEntity();
        _enemyArcher.PositionX = 1600;
        _enemyArcher.PositionY = 500;
        _enemyArcher.Direction = DirectionType.Left;

        var _enemyCleric = new EnemyClericEntity();
        _enemyCleric.PositionX = 1600;
        _enemyCleric.PositionY = 700;
        _enemyCleric.Direction = DirectionType.Left;

        _entitiesList.AddRange([_enemyWarrior, _enemyLancer, _enemyArcher, _enemyCleric]);
    }

    public override void Update()
    {
        base.Update();

        _entitiesList.ForEach(x => x.Update());
    }

    public override void Draw()
    {
        base.Draw();

        _entitiesList.ForEach(x => x.Draw());
    }
}
