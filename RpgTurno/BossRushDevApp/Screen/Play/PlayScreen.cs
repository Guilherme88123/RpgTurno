using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum.Component.Cursor;
using Domain.Model.Components.Base;
using Domain.Model.Components.Custom.Banners;
using Domain.Model.Entity.Units.Ally.Archer;
using Domain.Model.Entity.Units.Ally.Cleric;
using Domain.Model.Entity.Units.Ally.Lancer;
using Domain.Model.Entity.Units.Ally.Warrior;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using RpgTurno.CustomComponents.Background;
using RpgTurno.CustomComponents.Banners;
using RpgTurno.CustomComponents.DamageText;
using RpgTurno.CustomComponents.Selection;
using RpgTurno.CustomComponents.TurnQueue;
using RpgTurno.Screen.Play.Battle;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private readonly BattleManager _battleManager = new();

    private SelectionAreaComponent _selectionAreaComponent;
    private BaseUnitEntity _focusedEntity;
    private UnitBannerComponent _focusedUnitBannerComponent;

    private BackgroundComponent _backgroundImageComponent;

    private TurnQueueComponent _turnQueueComponent;
    private CurrentUnitTurnIndicatorComponent _currentTurnUnitComponent;

    private readonly List<DamageTextComponent> _damagesTextList = new();

    private AttackSelectBannerComponent _attackSelectComponent;

    //TODO: Adicionar componente para indicar Wave atual

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _battleManager.Initialize(CreateAllies());
        _battleManager.OnExecuteAttack += AddDamageText;

        _selectionAreaComponent = new();

        _focusedUnitBannerComponent = new();
        _focusedUnitBannerComponent.SetPosition(50, 400);

        _backgroundImageComponent = new();

        _turnQueueComponent = new();
        _currentTurnUnitComponent = new();

        //TODO: Implementar escolha de golpes ao atacar
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

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _battleManager.Update(gameTime);

        UpdateTurnComponents();

        UpdateDamageTexts(gameTime);

        VerifyCursorHoveringEntities();
    }

    #region Turn Components

    private void UpdateTurnComponents()
    {
        UpdateTurnQueueListComponent();
        UpdateCurrentTurnUnitComponent();
    }

    private void UpdateTurnQueueListComponent()
    {
        var ordenedUnitsList = _battleManager.GetUnitsTurnQueue();
        var spritesIconsList = ordenedUnitsList.Select(x => x.Icon).ToList();

        _turnQueueComponent.SetUnitsList(spritesIconsList);
    }

    private void UpdateCurrentTurnUnitComponent()
    {
        var currentTurnUnit = _battleManager.GetCurrentUnitTurnQueue();
        _currentTurnUnitComponent.SetCurrentTurnUnit(currentTurnUnit);
    }

    private void AddDamageText(BaseUnitEntity sender, BaseUnitEntity target)
    {
        var positionX = target.Center.X;
        var positionY = target.Center.Y;
        var damageText = $"-{sender.Damage}";

        _damagesTextList.Add(new DamageTextComponent((int)positionX, (int)positionY, damageText));
    }

    #endregion

    #region Focused Entity

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

        if (_battleManager.HasCursorHoveringEntity())
        {
            SetFocusedEntity(_battleManager.GetCursorHoveringEntity());
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
        _damagesTextList.RemoveAll(x => x.IsDestroyed);
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
        _battleManager.GetAllUnits().ForEach(x => x.Draw());
    }

    private void DrawDamageTexts()
    {
        _damagesTextList.ForEach(x => x.Draw(GlobalVariablesDto.SpriteBatchInterface));
    }

    #endregion
}
