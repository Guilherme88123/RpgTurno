using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Component.Cursor;
using Domain.Model.Components.Custom.Banners;
using Domain.Model.Entity.Units.Ally.Archer;
using Domain.Model.Entity.Units.Ally.Cleric;
using Domain.Model.Entity.Units.Ally.Lancer;
using Domain.Model.Entity.Units.Ally.Warrior;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Entity.Units.Enemy.Archer;
using Domain.Model.Entity.Units.Enemy.Cleric;
using Domain.Model.Entity.Units.Enemy.Lancer;
using Domain.Model.Entity.Units.Enemy.Warrior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RpgTurno.CustomComponents.Background;
using RpgTurno.CustomComponents.Selection;
using RpgTurno.CustomComponents.TurnQueue;
using RpgTurno.Screen.Play.Attack;
using RpgTurno.Screen.Play.Delay;
using RpgTurno.Screen.Play.Turn;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private List<BaseUnitEntity> _alliesParty = new();
    private List<BaseUnitEntity> _enemiesParty = new();
    private List<BaseUnitEntity> _allUnits => [.. _alliesParty, .. _enemiesParty];

    private SelectionAreaComponent _selectionArea;
    private BaseUnitEntity _focusedEntity;
    private UnitBannerComponent _focusedUnitBanner;

    private BackgroundComponent _backgroundImage;

    private TurnQueueManager _turnQueueManager = new();
    private TurnQueueComponent _turnQueueComponent;
    private CurrentUnitTurnIndicatorComponent _currentTurnUnitComponent;

    private AttackManager _attackManager = new();

    private DelayManager _delayManager = new();

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

        _focusedUnitBanner = new();
        _focusedUnitBanner.SetPosition(50, 400);

        _backgroundImage = new();

        _turnQueueComponent = new();
        _currentTurnUnitComponent = new();
        _turnQueueManager.SetUnitsQueue(_allUnits);
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

    private void SetEntitiesYPosition(List<BaseUnitEntity> entitiesList)
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

    private void FixEntitiesPositionBySize(List<BaseUnitEntity> entitiesList)
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

        UpdateDelayManager();

        UpdateTurn();

        _alliesParty.ForEach(x => x.Update());
        _enemiesParty.ForEach(x => x.Update());

        _selectionArea.Update(gameTime);
        _focusedUnitBanner.Update(gameTime);

        VerifyCursorHoveringEntities();
    }

    #region Turn

    private void UpdateTurn()
    {
        UpdateTurnComponent();
        UpdateTurnAction();
    }

    #region Action

    private void UpdateTurnAction()
    {
        if (HasPendingAttack())
        {
            if (HasDelayAttackFinished())
                ExecuteAttack();

            return;
        }

        if (!HasDelayTurnFinished())
            return;

        var currentUnit = _turnQueueManager.GetPeekUnit();

        if (IsEnemyUnit(currentUnit))
            UpdateEnemyTurn(currentUnit);
        else
            UpdateAllyTurn(currentUnit);
    }

    private bool HasPendingAttack()
    {
        return _attackManager.HasPendingAttack();
    }

    private bool IsEnemyUnit(BaseUnitEntity unit)
    {
        return _enemiesParty.Contains(unit);
    }

    private void UpdateEnemyTurn(BaseUnitEntity enemyUnit)
    {
        var targetAlly = _alliesParty.Shuffle().First();

        StartAttack(enemyUnit, targetAlly);
    }

    private void UpdateAllyTurn(BaseUnitEntity allyUnit)
    {
        if (GlobalVariablesDto.PreviousMouseDown)
            return;

        if (GlobalVariablesDto.MouseState.LeftButton != ButtonState.Pressed)
            return;

        if (!HasCursorHoveringEntity())
            return;

        var targetEnemy = GetCursorHoveringEntity();

        if (_alliesParty.Contains(targetEnemy))
            return;

        StartAttack(allyUnit, targetEnemy);
    }

    private void StartAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        sender.CreatureState = CreatureStateType.Attacking;

        ResetDelayAttack();

        _attackManager.StartAttack(sender, target);
    }

    private void ExecuteAttack()
    {
        var (sender, target) = _attackManager.ExecuteAttack();

        if (target.Health <= 0)
        {
            RemoveUnit(target);
        }

        sender.CreatureState = CreatureStateType.Idle;

        _turnQueueManager.NextTurn();

        ResetDelayTurn();
    }

    private void RemoveUnit(BaseUnitEntity unit)
    {
        _alliesParty.Remove(unit);
        _enemiesParty.Remove(unit);
        _turnQueueManager.RemoveUnit(unit);
    }

    private void ResetDelayTurn()
    {
        _delayManager.ResetDelayTurnExecution();
    }

    private bool HasDelayTurnFinished()
    {
        return _delayManager.HasDelayTurnExecutionComplete();
    }

    private void ResetDelayAttack()
    {
        _delayManager.ResetDelayAttackExecution();
    }

    private bool HasDelayAttackFinished()
    {
        return _delayManager.HasDelayAttackExecutionComplete();
    }

    #endregion

    #region Component

    private void UpdateTurnComponent()
    {
        UpdateTurnQueueListComponent();
        UpdateCurrentTurnUnitComponent();
    }

    private void UpdateTurnQueueListComponent()
    {
        var ordenedUnitsList = _turnQueueManager.GetUnitQueueList();
        var spritesIconsList = ordenedUnitsList.Select(x => x.Icon).ToList();

        _turnQueueComponent.SetUnitsList(spritesIconsList);
    }

    private void UpdateCurrentTurnUnitComponent()
    {
        var currentTurnUnit = _turnQueueManager.GetPeekUnit();
        _currentTurnUnitComponent.SetCurrentTurnUnit(currentTurnUnit);
    }

    #endregion

    #endregion

    #region Focused Entity

    private bool HasCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _allUnits.Any(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    private BaseUnitEntity GetCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _allUnits.First(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    private void SetFocusedEntity(BaseUnitEntity entity)
    {
        _focusedEntity = entity;
        _selectionArea.SetDestinationRectangle(entity.Rectangle);
        _focusedUnitBanner.SetFocusedUnit(entity);
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

    #region Hover

    private void VerifyCursorHoveringEntities()
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

    #endregion

    #region Delay

    private void UpdateDelayManager()
    {
        _delayManager.Update();
    }

    #endregion

    #endregion

    #region Draw

    public override void Draw()
    {
        DrawBackground();

        DrawAllies();
        DrawEnemies();

        DrawTurnQueue();
        DrawCurrentTurnUnitIndicator();

        if (HasFocusedEntity())
        {
            DrawSelectionAreaOnFocusedEntity();
            DrawFocusedEntityBanner();
        }

        base.Draw();
    }

    private void DrawBackground()
    {
        _backgroundImage.Draw(GlobalVariablesDto.SpriteBatchBackground);
    }

    private void DrawAllies()
    {
        _alliesParty.ForEach(x => x.Draw());
    }

    private void DrawEnemies()
    {
        _enemiesParty.ForEach(x => x.Draw());
    }

    private void DrawCurrentTurnUnitIndicator()
    {
        _currentTurnUnitComponent.Draw(GlobalVariablesDto.SpriteBatchInterface);
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
        _focusedUnitBanner.Draw(GlobalVariablesDto.SpriteBatchInterface);
    }

    private void DrawTurnQueue()
    {
        _turnQueueComponent.Draw(GlobalVariablesDto.SpriteBatchInterface);
    }

    #endregion
}
