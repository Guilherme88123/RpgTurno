using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Ally.Archer;
using Domain.Model.Entity.Units.Ally.Cleric;
using Domain.Model.Entity.Units.Ally.Lancer;
using Domain.Model.Entity.Units.Ally.Warrior;
using Domain.Model.Entity.Units.Enemy.Archer;
using Domain.Model.Entity.Units.Enemy.Cleric;
using Domain.Model.Entity.Units.Enemy.Lancer;
using Domain.Model.Entity.Units.Enemy.Warrior;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurnoApp.Screen;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private List<BaseEntity> _alliesParty = new();
    private List<BaseEntity> _enemiesParty = new();

    public override void Initialize()
    {
        base.Initialize();

        var _warrior = new WarriorEntity();
        var _warrior2 = new WarriorEntity();
        var _lancer = new LancerEntity();
        var _archer = new ArcherEntity();
        var _cleric = new ClericEntity();

        _alliesParty.AddRange([_warrior, _warrior2, _lancer, _archer, _cleric]);

        var _enemyWarrior = new EnemyWarriorEntity();
        var _enemyLancer = new EnemyLancerEntity();
        var _enemyArcher = new EnemyArcherEntity();
        var _enemyCleric = new EnemyClericEntity();

        _enemiesParty.AddRange([_enemyWarrior, _enemyLancer, _enemyArcher, _enemyCleric]);

        UpdateEntitiesPosition();
    }

    public override void Update()
    {
        base.Update();

        _alliesParty.ForEach(x => x.Update());
        _enemiesParty.ForEach(x => x.Update());
    }

    public override void Draw()
    {
        base.Draw();

        _alliesParty.ForEach(x => x.Draw());
        _enemiesParty.ForEach(x => x.Draw());
    }

    private void UpdateEntitiesPosition()
    {
        UpdateAlliesPosition();
        UpdateEnemiesPosition();
    }

    private void UpdateAlliesPosition()
    {
        int posX = GlobalOptionsDto.WidthSize / 3;

        _alliesParty.ForEach(x => x.PositionX = posX);

        SetEntitiesYPosition(_alliesParty);
    }

    private void UpdateEnemiesPosition()
    {
        int posX = (GlobalOptionsDto.WidthSize / 3) * 2;

        _enemiesParty.ForEach(x => x.PositionX = posX);
        _enemiesParty.ForEach(x => x.Direction = DirectionType.Left);

        SetEntitiesYPosition(_enemiesParty);
    }

    private void SetEntitiesYPosition(List<BaseEntity> entitiesList)
    {
        if (!entitiesList.Any()) return;

        int entityHeight = 150;
        int fixedTopMargin = 50;
        int margin = 30;
        int step = entityHeight + margin;
        int totalHeight = entitiesList.Count * entityHeight + (entitiesList.Count - 1) * margin;
        int initialY = GlobalOptionsDto.HeightSize / 2 - totalHeight / 2 + fixedTopMargin;

        foreach (var (entity, index) in entitiesList.Select((e, i) => (e, i)))
        {
            entity.PositionY = initialY + step * index;
        }
    }
}
