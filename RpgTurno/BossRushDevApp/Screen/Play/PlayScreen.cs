using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum.Battle;
using Domain.Enum.Component.Cursor;
using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Custom.CustomComponents.Play.Background;
using RpgTurno.Custom.CustomComponents.Play.Banners;
using RpgTurno.Custom.CustomComponents.Play.Selection;
using RpgTurno.Custom.CustomComponents.Play.TurnQueue;
using RpgTurno.Custom.CustomComponents.Play.Wave;
using RpgTurno.Screen.Play.Battle;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Play;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private readonly BattleManager _battleManager = new();

    private SelectionAreaComponent _selectionAreaComponent;
    private BaseUnitEntity _focusedEntity;
    private UnitBannerComponent _focusedUnitBannerComponent;

    private PlayBackgroundComponent _backgroundImageComponent;

    private TurnQueueComponent _turnQueueComponent;
    private CurrentUnitTurnIndicatorComponent _currentTurnUnitComponent;

    private readonly List<PositionableAnimation> _skillAnimationsList = new();

    private SkillSelectBannerComponent _skillSelectComponent;

    private WaveIndicatorComponent _waveIndicatorComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _battleManager.Initialize(GetAllies(), GameSession.CurrentStageCode);
        _battleManager.OnTurnStart += OnTurnStart;
        _battleManager.OnTurnFinish += OnTurnFinish;
        _battleManager.OnBattleFinish += BattleFinish;
        _battleManager.OnPlaySenderAnimation += AddAnimation;
        _battleManager.OnPlayTargetsAnimation += AddAnimation;

        _selectionAreaComponent = new();

        _focusedUnitBannerComponent = new();
        _focusedUnitBannerComponent.SetPosition(50, 300);

        _backgroundImageComponent = new();

        _turnQueueComponent = new();
        _currentTurnUnitComponent = new();

        _skillSelectComponent = new();
        _skillSelectComponent.SetPosition(30, GlobalOptionsDto.HeightSize - _skillSelectComponent.Bounds.Height - 30);
        _skillSelectComponent.OnSkillSelect = SetSelectedSkill;
        _skillSelectComponent.IsVisible = false;

        _waveIndicatorComponent = new();
        _waveIndicatorComponent.SetPosition(GlobalOptionsDto.WidthSize - 140, 30);

        return new() {
            _selectionAreaComponent,
            _focusedUnitBannerComponent,
            _turnQueueComponent,
            _currentTurnUnitComponent,
            _skillSelectComponent,
            _waveIndicatorComponent,
        };
    }

    private List<BaseUnitEntity> GetAllies()
    {
        return GameSession.Allies;
    }

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _battleManager.Update(gameTime);

        UpdateTurnComponents();
        UpdateWaveIndicator();
        UpdateSkillAnimations();

        VerifyCursorHoveringEntities();
        VerifyExitingStage();
    }

    #region Turn Components

    private void UpdateTurnComponents()
    {
        SeTurnComponentVisibilityByBattleState();
        UpdateTurnQueueListComponent();
        UpdateCurrentTurnUnitComponent();
        UpdateSelectSkillComponent();
    }

    private void SeTurnComponentVisibilityByBattleState()
    {
        var battleState = _battleManager.BattleState;
        var isFighting = battleState != BattleState.WaveTransition;

        _turnQueueComponent.IsVisible = isFighting;
        _currentTurnUnitComponent.IsVisible = isFighting;
    }

    private void UpdateTurnQueueListComponent()
    {
        _turnQueueComponent.SetUnitsList(_battleManager.GetUnitsTurnQueue());
    }

    private void UpdateCurrentTurnUnitComponent()
    {
        var currentTurnUnit = _battleManager.GetCurrentUnitTurnQueue();
        _currentTurnUnitComponent.SetCurrentTurnUnit(currentTurnUnit);
    }

    private void OnTurnFinish(BaseUnitEntity sender, BaseUnitEntity target)
    {
        _turnQueueComponent.StartTransition();
    }

    private void OnTurnStart(BaseUnitEntity sender, bool isEnemy)
    {
        if (isEnemy)
            return;

        _skillSelectComponent.SetUnit(sender);
    }

    #endregion

    #region Skill

    #region Skill Animation

    private void UpdateSkillAnimations()
    {
        List<PositionableAnimation> toRemoveAnimations = new();

        foreach (var skillAnimation in _skillAnimationsList)
        {
            if (skillAnimation.Animation.IsFinished)
                toRemoveAnimations.Add(skillAnimation);

            skillAnimation.Update();
        }

        toRemoveAnimations.ForEach(x => _skillAnimationsList.Remove(x));
    }

    private void AddAnimation(List<BaseUnitEntity> units, AnimationClip animation)
    {
        foreach (var unit in units)
            AddAnimation(unit, animation);
    }

    private void AddAnimation(BaseUnitEntity unit, AnimationClip animation)
    {
        animation.IsLoop = false;
        _skillAnimationsList.Add(new PositionableAnimation(unit.Rectangle, animation));
    }

    #endregion

    #region Skill Select

    private void UpdateSelectSkillComponent()
    {
        _skillSelectComponent.IsVisible = _battleManager.CanSelectSkill;
    }

    private void SetSelectedSkill(UnitSkill skill)
    {
        _battleManager.SetPlayerSelectedSkill(skill);
    }

    #endregion

    #endregion

    #region Focused Entity

    private void SetFocusedEntity(BaseUnitEntity entity)
    {
        _focusedEntity = entity;
        _selectionAreaComponent.SetDestinationRectangle(entity.Rectangle);
        _focusedUnitBannerComponent.SetFocusedUnit(entity, _battleManager.IsEnemyUnit(entity));
        _turnQueueComponent.SetFocusedUnit(entity);
    }

    private void ClearFocusedEntity()
    {
        _focusedEntity = null;
        _turnQueueComponent.ClearFocusedUnit();
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

        if (_skillSelectComponent.HasCursorHoveringButton() && _skillSelectComponent.IsVisible)
        {
            OnHoverInSkillButtonAction(_skillSelectComponent.CanUseFocusedButton());
            return;
        }

        if (_battleManager.HasCursorHoveringEntity())
        {
            OnHoverInUnitAction(_battleManager.GetCursorHoveringEntity());
            return;
        }

        if (_turnQueueComponent.HasCursorHoveringEntity())
        {
            OnHoverInUnitAction(_turnQueueComponent.GetCursorHoveringEntity());
            return;
        }

        OnHoverOutAction();
    }

    private void OnHoverInUnitAction(BaseUnitEntity unit)
    {
        SetFocusedEntity(unit);
        SetHoverCursor();
    }

    private void OnHoverInSkillButtonAction(bool canUse)
    {
        if (canUse)
            SetHoverCursor();
        else
            SetBlockCursor();
    }

    private void OnHoverOutAction()
    {
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

    #region Wave Indicator

    private void UpdateWaveIndicator()
    {
        var currentWave = _battleManager.GetCurrentWaveIndex();
        var totalWaves = _battleManager.GetTotalCountWaves();

        _waveIndicatorComponent.SetWavesNumber(currentWave, totalWaves);
    }

    #endregion

    #region Return To Map Screen

    private void VerifyExitingStage()
    {
        var teclado = Keyboard.GetState();

        if (teclado.IsKeyDown(Keys.Escape))
            BattleFinish(isGameOver: true);
    }

    private void BattleFinish(bool isGameOver = false)
    {
        GameSession.IsInBattle = false;

        if (!isGameOver)
            GameSession.OnStageCleared?.Invoke();

        GlobalVariablesDto.PopScreen();
        GlobalVariablesDto.ResetFollow(GlobalVariablesDto.SpriteBatchBackground);
    }

    #endregion

    #endregion

    #region Draw

    public override void Draw()
    {
        DrawBackground();
        DrawBattle();
        DrawSkillAnimations();

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

    private void DrawSkillAnimations()
    {
        _skillAnimationsList.ForEach(x => x.Draw());
    }

    #endregion
}

public class PositionableAnimation
{
    public Rectangle Rectangle { get; set; }
    public AnimationClip Animation { get; set; }

    public PositionableAnimation(Rectangle rectangle, AnimationClip animation)
    {
        Rectangle = rectangle;
        Animation = animation;
    }

    public void Update()
    {
        Animation.Update();
    }

    public void Draw()
    {
        Animation.Draw(Rectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchInterface);
    }
}
