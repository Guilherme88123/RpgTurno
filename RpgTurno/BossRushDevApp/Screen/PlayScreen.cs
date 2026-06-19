using Accessibility;
using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Component.Cursor;
using Domain.Model.Components.Custom.Banners;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Ally.Archer;
using Domain.Model.Entity.Units.Ally.Cleric;
using Domain.Model.Entity.Units.Ally.Lancer;
using Domain.Model.Entity.Units.Ally.Warrior;
using Domain.Model.Entity.Units.Enemy.Archer;
using Domain.Model.Entity.Units.Enemy.Cleric;
using Domain.Model.Entity.Units.Enemy.Lancer;
using Domain.Model.Entity.Units.Enemy.Warrior;
using Microsoft.Xna.Framework;
using RpgTurno.CustomComponents.Selection;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurnoApp.Screen;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private List<BaseEntity> _alliesParty = new();
    private List<BaseEntity> _enemiesParty = new();
    private List<BaseEntity> _allEntities => [.. _alliesParty, .. _enemiesParty];

    private SelectionAreaComponent _selectionArea;
    private BaseEntity _focusedEntity;
    private EntitytBannerComponent _focusedEntitybanner;

    #region Initialize

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

        SetEntitiesPosition();

        _selectionArea = new();
        _focusedEntitybanner = new (new Rectangle(0, 0, 500, 300));
    }

    private void SetEntitiesPosition()
    {
        SetAlliesPosition();
        SetEnemiesPosition();
    }

    private void SetAlliesPosition()
    {
        int posX = GlobalOptionsDto.WidthSize / 3;

        _alliesParty.ForEach(x => x.PositionX = posX);

        SetEntitiesYPosition(_alliesParty);
        FixEntitiesPositionBySize(_alliesParty);
    }

    private void SetEnemiesPosition()
    {
        int posX = (GlobalOptionsDto.WidthSize / 3) * 2;

        _enemiesParty.ForEach(x => x.PositionX = posX);
        _enemiesParty.ForEach(x => x.Direction = DirectionType.Left);

        SetEntitiesYPosition(_enemiesParty);
        FixEntitiesPositionBySize(_enemiesParty);
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

    private void FixEntitiesPositionBySize(List<BaseEntity> entitiesList)
    {
        if (!entitiesList.Any()) return;

        foreach (var entity in entitiesList)
        {
            entity.PositionX -= entity.SizeX / 2;
            entity.PositionY -= entity.SizeY / 2;
        }
    }

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _alliesParty.ForEach(x => x.Update());
        _enemiesParty.ForEach(x => x.Update());

        VerifyCursorHoverEntities();
    }

    private void VerifyCursorHoverEntities()
    {
        if (HasCursorHoveringEntity())
        {
            SetFocusedEntity(GetCursorHoveringEntity());
            SetHoverCursor();
            return;
        }

        ClearFocusedEntity();
        SetNormalCursor();
    }

    #region Focused Entity

    private bool HasCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _allEntities.Any(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    private BaseEntity GetCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _allEntities.First(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    private void SetFocusedEntity(BaseEntity entity)
    {
        _focusedEntity = entity;
        _selectionArea.SetDestinationRectangle(entity.Rectangle);
    }

    private void ClearFocusedEntity()
    {
        _focusedEntity = null;
    }

    #endregion

    #region Cursor

    private void SetNormalCursor()
    {
        CursorComponent.SetCursorState(CursorStateType.Normal);
    }

    private void SetHoverCursor()
    {
        CursorComponent.SetCursorState(CursorStateType.Hover);
    }

    private void SetBlockCursor()
    {
        CursorComponent.SetCursorState(CursorStateType.Block);
    }

    #endregion

    #endregion

    #region Draw

    public override void Draw()
    {
        base.Draw();

        DrawAllies();
        DrawEnemies();

        if (HasFocusedEntity())
        {
            DrawSelectionAreaOnFocusedEntity();
            DrawFocusedEntityBanner();
        }
    }

    private void DrawAllies()
    {
        _alliesParty.ForEach(x => x.Draw());
    }

    private void DrawEnemies()
    {
        _enemiesParty.ForEach(x => x.Draw());
    }

    private bool HasFocusedEntity()
    {
        return _focusedEntity is not null;
    }

    private void DrawSelectionAreaOnFocusedEntity()
    {
        _selectionArea.Draw(GlobalVariablesDto.SpriteBatchInterface);
    }

    private void DrawFocusedEntityBanner()
    {
        _focusedEntitybanner.Draw(GlobalVariablesDto.SpriteBatchInterface);
    }

    #endregion
}
