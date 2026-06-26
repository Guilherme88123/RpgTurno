using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Enum.Attack;
using Domain.Enum.Component.Cursor;
using Domain.Model.Components.Base;
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
using RpgTurno.CustomComponents.Banners;
using RpgTurno.CustomComponents.DamageText;
using RpgTurno.CustomComponents.Selection;
using RpgTurno.CustomComponents.TurnQueue;
using RpgTurno.Screen.Play.Attack;
using RpgTurno.Screen.Play.Battle;
using RpgTurno.Screen.Play.Turn;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private BattleManager _battleManager = new();

    private SelectionAreaComponent _selectionAreaComponent;
    private BaseUnitEntity _focusedEntity;
    private UnitBannerComponent _focusedUnitBannerComponent;

    private BackgroundComponent _backgroundImageComponent;

    private TurnQueueComponent _turnQueueComponent;
    private CurrentUnitTurnIndicatorComponent _currentTurnUnitComponent;

    private readonly List<DamageTextComponent> _damagesTextList = new();

    private AttackSelectBannerComponent _attackSelectComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _battleManager.Initialize(CreateAllies());

        _selectionAreaComponent = new();

        _focusedUnitBannerComponent = new();
        _focusedUnitBannerComponent.SetPosition(50, 400);

        _backgroundImageComponent = new();

        _turnQueueComponent = new();
        _currentTurnUnitComponent = new();

        _attackSelectComponent = new();
        _attackSelectComponent.SetPosition(30, GlobalOptionsDto.HeightSize - _attackSelectComponent.Bounds.Height - 30);
        _attackSelectComponent.IsVisible = false;

        return new() {
            _selectionAreaComponent, 
            _focusedUnitBannerComponent, 
            _turnQueueComponent,
            _currentTurnUnitComponent,
            _attackSelectComponent,
        };
    }

    public override void Initialize()
    {
        base.Initialize();

        SetEntitiesPosition();

        _turnQueueManager.SetUnitsQueue(AllUnits);
    }

    private List<BaseUnitEntity> CreateAllies()
    {
        return
        [
            new WarriorEntity(),
            new ArcherEntity(),
            new LancerEntity(),
            new ClericEntity(),
        ];
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

        UpdateTurn();

        _selectionAreaComponent.Update(gameTime);
        _focusedUnitBannerComponent.Update(gameTime);

        _battleManagar.Update(gameTime);

        UpdateDamageTexts(gameTime);

        VerifyCursorHoveringEntities();

        VerifyEndOfGame();
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
        UpdateAttackManager();

        switch (GetCurrentAttackPhase())
        {
            case AttackPhase.Idle:
                HandleIdlePhase();
                break;
            case AttackPhase.MovingToTarget:
                HandleMovingToTargetPhase();
                break;
            case AttackPhase.Attacking:
                HandleAttackingPhase();
                break;
            case AttackPhase.MovingBack:
                HandleMovingBackPhase();
                break;
            case AttackPhase.WaitingTurn:
                HandleWaitingTurnPhase();
                break;
        }
    }

    private void UpdateAttackManager()
    {
        _attackManager.Update();
    }

    private AttackPhase GetCurrentAttackPhase()
    {
        return _attackManager.CurrentPhase;
    }

    private void HandleIdlePhase()
    {
        var currentUnit = _turnQueueManager.GetPeekUnit();

        if (IsEnemyUnit(currentUnit))
            UpdateEnemyTurn(currentUnit);
        else
            UpdateAllyTurn(currentUnit);
    }

    private void HandleMovingToTargetPhase()
    {
        _attackManager.UpdateMovingToTarget();
    }

    private void HandleAttackingPhase()
    {
        if (!_attackManager.HasAttackFinished())
            return;

        ExecuteAttack();
    }

    private void HandleMovingBackPhase()
    {
        _attackManager.UpdateMovingBack();
    }

    private void HandleWaitingTurnPhase()
    {
        if (!_attackManager.HasWaitTurnFinished())
            return;

        _attackManager.Reset();

        _turnQueueManager.NextTurn();
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
        //TODO: Implementar escolha de golpes ao atacar
        //_attackSelectComponent.IsVisible = true;

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

        _attackSelectComponent.IsVisible = false;
    }

    private void StartAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        _attackManager.StartAttack(sender, target, IsEnemyUnit(sender));
    }

    private void ExecuteAttack()
    {
        var (sender, target) = _attackManager.ExecuteAttack();

        AddDamageText(sender, target);

        if (target.IsDestroyed)
            RemoveUnit(target);
    }

    private void AddDamageText(BaseUnitEntity sender, BaseUnitEntity target)
    {
        var positionX = target.Center.X;
        var positionY = target.Center.Y;
        var damageText = $"-{sender.Damage}";

        _damagesTextList.Add(new DamageTextComponent((int)positionX, (int)positionY, damageText));
    }

    private void RemoveUnit(BaseUnitEntity unit)
    {
        _alliesParty.Remove(unit);
        _enemiesParty.Remove(unit);
        _turnQueueManager.RemoveUnit(unit);
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
        return AllUnits.Any(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    private BaseUnitEntity GetCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return AllUnits.First(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    private void SetFocusedEntity(BaseUnitEntity entity)
    {
        _focusedEntity = entity;
        _selectionAreaComponent.SetDestinationRectangle(entity.Rectangle);
        _focusedUnitBannerComponent.SetFocusedUnit(entity);
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
        UpdateFocusedUnitComponentsVisibility();

        if (HasCursorHoveringEntity())
        {
            SetFocusedEntity(GetCursorHoveringEntity());
            SetHoverCursor();
            return;
        }

        ClearFocusedEntity();
        SetNormalCursor();
    }

    private void UpdateFocusedUnitComponentsVisibility()
    {
        bool hasFocusedUnit = HasFocusedEntity();

        _focusedUnitBannerComponent.IsVisible = hasFocusedUnit;
        _selectionAreaComponent.IsVisible = hasFocusedUnit;
    }

    private bool HasFocusedEntity()
    {
        return _focusedEntity is not null;
    }

    #endregion

    #region Damage Texts

    private void UpdateDamageTexts(GameTime gameTime)
    {
        _damagesTextList.ForEach(x => x.Update(gameTime));
        _damagesTextList.RemoveAll(x => x.IsDestroied);
    }

    #endregion

    #region Game End

    private void VerifyEndOfGame()
    {
        if (IsEnemyPartyEmpty() || IsAllyPartyEmpty())
            GlobalVariablesDto.ChangeScreen.Invoke(ScreenConst.PlayScreen);
    }

    private bool IsEnemyPartyEmpty()
    {
        return !_enemiesParty.Any();
    }

    private bool IsAllyPartyEmpty()
    {
        return !_alliesParty.Any();
    }

    #endregion

    #endregion

    #region Draw

    public override void Draw()
    {
        DrawBackground();
        DrawBattle();
        DrawDamageTexts();
        
        base.Draw();
    }

    private void DrawBackground()
    {
        _backgroundImageComponent.Draw(GlobalVariablesDto.SpriteBatchBackground);
    }

    private void DrawBattle()
    {
        _battleManagar.GetAllUnits().ForEach(x => x.Draw());
    }

    private void DrawDamageTexts()
    {
        _damagesTextList.ForEach(x => x.Draw(GlobalVariablesDto.SpriteBatchInterface));
    }

    #endregion
}
