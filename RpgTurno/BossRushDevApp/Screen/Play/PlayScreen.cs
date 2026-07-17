using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum.Battle;
using Domain.Enum.Component.Cursor;
using Domain.Interface.Cursor;
using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Custom.Component.Play.Banners.Pause;
using RpgTurno.Custom.Component.Play.Skill;
using RpgTurno.Custom.CustomComponents.Play.Background;
using RpgTurno.Custom.CustomComponents.Play.Banners;
using RpgTurno.Custom.CustomComponents.Play.Selection;
using RpgTurno.Custom.CustomComponents.Play.TurnQueue;
using RpgTurno.Custom.CustomComponents.Play.Wave;
using RpgTurno.Screen.Play.Battle;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play;

public class PlayScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.PlayScreen;

    private ICursorManager _cursorManager => GlobalVariablesDto.GetService<ICursorManager>();

    private KeyboardState _previousKeyboardState;
    private bool _isPaused;
    private Keys _pauseKey = Keys.Escape;
    private PlayPauseBannerComponent _pauseBannerComponent;

    private BattleManager _battleManager;

    private readonly List<BaseUnitEntity> _focusedUnitsList = new();
    private SelectionAreaComponent _selectionAreaComponent;
    private UnitBannerComponent _focusedUnitBannerComponent;

    private BattleMapBackgroundComponent _backgroundImageComponent;

    private TurnQueueComponent _turnQueueComponent;
    private CurrentUnitTurnIndicatorComponent _currentTurnUnitComponent;

    private readonly List<PositionableAnimation> _skillAnimationsList = new();

    private SkillSelectBannerComponent _skillSelectComponent;
    private UsedSkillIndicatorComponent _usedSkillIndicator;

    private WaveIndicatorComponent _waveIndicatorComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        InitializeBattleManager();

        _selectionAreaComponent = new();

        _focusedUnitBannerComponent = new();
        _focusedUnitBannerComponent.SetPosition(70, 48);

        _backgroundImageComponent = new();

        _turnQueueComponent = new();
        _currentTurnUnitComponent = new();

        _skillSelectComponent = new();
        _skillSelectComponent.SetPosition(30, GlobalOptionsDto.HeightSize - _skillSelectComponent.Bounds.Height - 30);
        _skillSelectComponent.OnSkillSelect = SetSelectedSkill;
        _skillSelectComponent.IsVisible = false;

        _usedSkillIndicator = new();
        _usedSkillIndicator.SetPosition(GlobalOptionsDto.WidthSize / 2 - _usedSkillIndicator.Bounds.Width / 2, 112);

        _waveIndicatorComponent = new();
        _waveIndicatorComponent.SetPosition(GlobalOptionsDto.WidthSize - 140, 30);

        _pauseBannerComponent = new(
            onResumeAction: OnResumeAction,
            onOptionsAction: OnOptionsAction,
            onRestartAction: OnRestartAction,
            onMapAction: OnMapAction);
        _pauseBannerComponent.IsVisible = false;
        _pauseBannerComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _pauseBannerComponent.Bounds.Width / 2,
            GlobalOptionsDto.HeightSize / 2 - _pauseBannerComponent.Bounds.Height / 2);

        return new() {
            _focusedUnitBannerComponent,
            _turnQueueComponent,
            _currentTurnUnitComponent,
            _skillSelectComponent,
            _usedSkillIndicator,
            _waveIndicatorComponent,
            _pauseBannerComponent,
        };
    }

    private void InitializeBattleManager()
    {
        _battleManager = new();
        _battleManager.Initialize(GetAllies(), GameSession.CurrentStageCode);
        _battleManager.OnTurnStart += OnTurnStart;
        _battleManager.OnTurnFinish += OnTurnFinish;
        _battleManager.OnBattleFinish += BattleFinish;
        _battleManager.OnPlaySenderAnimation += AddAnimation;
        _battleManager.OnPlayTargetsAnimation += AddAnimation;
        _battleManager.OnSkillSelect += HandleSkillSelect;
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

        VerifyPause();
        UpdatePauseFlag();

        if (_isPaused)
            return;

        _battleManager.Update(gameTime);

        UpdateUsedSkillComponentVisibility(gameTime);
        UpdateSelectionAreaComponent(gameTime);
        UpdateTurnComponents();
        UpdateWaveIndicator();
        UpdateSkillAnimations();

        VerifyCursorHoveringEntities();
    }

    #region Pause

    private void UpdatePauseFlag()
    {
        _pauseBannerComponent.IsVisible = _isPaused;
        _pauseBannerComponent.IsEnable = _isPaused;

        _selectionAreaComponent.IsEnable = !_isPaused;
        _selectionAreaComponent.IsVisible = !_isPaused;

        _focusedUnitBannerComponent.IsEnable = !_isPaused;
        _focusedUnitBannerComponent.IsVisible = !_isPaused;

        _turnQueueComponent.IsEnable = !_isPaused;
        _turnQueueComponent.IsVisible = !_isPaused;

        _currentTurnUnitComponent.IsEnable = !_isPaused;
        _currentTurnUnitComponent.IsVisible = !_isPaused;

        _skillSelectComponent.IsEnable = !_isPaused;
        _skillSelectComponent.IsVisible = !_isPaused;
    }

    private void VerifyPause()
    {
        bool isPauseKeyPressed = IsPauseKeyPressed();

        if (isPauseKeyPressed)
            TogglePauseFlag();

        _previousKeyboardState = GlobalVariablesDto.KeyboardState;
    }

    private void TogglePauseFlag()
    {
        _isPaused = !_isPaused;
    }

    private bool IsPauseKeyPressed()
    {
        return GlobalVariablesDto.KeyboardState.IsKeyDown(_pauseKey) && _previousKeyboardState.IsKeyUp(_pauseKey);
    }

    #endregion

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
        animation.IsLoop = false;
        _skillAnimationsList.Add(new PositionableAnimation(units, animation));
    }

    private void AddAnimation(BaseUnitEntity unit, AnimationClip animation)
    {
        AddAnimation([unit], animation);
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

    private void HandleSkillSelect(UnitSkill skill)
    {
        _usedSkillIndicator.SetUsedSkill(skill, _battleManager.IsEnemyUnit(skill.OwnerUnit));
    }

    private void UpdateUsedSkillComponentVisibility(GameTime gameTime)
    {
        _usedSkillIndicator.IsVisible = _battleManager.IsAttacking;
    }

    #endregion

    #endregion

    #region Focused Entity

    private void SetFocusedEntity(BaseUnitEntity unit)
    {
        _focusedUnitsList.Clear();
        _focusedUnitsList.Add(unit);

        _focusedUnitBannerComponent.SetFocusedUnit(unit, _battleManager.IsEnemyUnit(unit));
        _turnQueueComponent.SetFocusedUnit(unit);
    }

    private void SetFocusedUnit(List<BaseUnitEntity> allUnits, BaseUnitEntity principalUnit)
    {
        _focusedUnitsList.Clear();
        _focusedUnitsList.AddRange(allUnits);

        _focusedUnitBannerComponent.SetFocusedUnit(principalUnit, _battleManager.IsEnemyUnit(principalUnit));
        _turnQueueComponent.SetFocusedUnit(principalUnit);
    }

    private void ClearFocusedEntity()
    {
        _focusedUnitsList.Clear();
        _turnQueueComponent.ClearFocusedUnit();
    }

    #region Select Component

    private void UpdateSelectionAreaComponent(GameTime gameTime)
    {
        _selectionAreaComponent.Update(gameTime);
    }

    #endregion

    #endregion

    #region Cursor

    private void SetHoverCursor()
    {
        _cursorManager.RequestHover();
    }

    private void SetBlockCursor()
    {
        _cursorManager.RequestBlock();
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
        if (IsSkillSelected() && IsCurrentUnitTurnAnAlly())
        {
            OnHoverUnitWithSkill(unit);
            return;
        }

        SetFocusedEntity(unit);
        SetHoverCursor();
    }

    private bool IsSkillSelected()
    {
        return _battleManager.SelectedSkill is not null;
    }

    private bool IsCurrentUnitTurnAnAlly()
    {
        return !_battleManager.IsEnemyUnit(_battleManager.CurrentTurnUnit);
    }

    private void OnHoverUnitWithSkill(BaseUnitEntity unit)
    {
        var avaliableTargets = _battleManager.GetAvaliableTargets();

        if (avaliableTargets.Contains(unit))
        {
            SetFocusedUnit(_battleManager.GetTargetsBySelectedUnit(unit, avaliableTargets), unit);
            SetHoverCursor();
            return;
        }

        SetFocusedEntity(unit);
        SetBlockCursor();
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
    }

    private void UpdateFocusedUnitComponentsVisibility()
    {
        bool hasFocusedUnit = HasFocusedEntity();

        _focusedUnitBannerComponent.IsVisible = hasFocusedUnit;
        _selectionAreaComponent.IsVisible = hasFocusedUnit;
    }

    private bool HasFocusedEntity()
    {
        return _focusedUnitsList.Any();
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
        DrawSkillAnimations();

        if (_isPaused)
            DrawPausedShade();

        base.Draw();
        DrawSelectionAreaComponent();

        DrawBattle();
    }

    private void DrawBackground()
    {
        _backgroundImageComponent.Draw(GlobalVariablesDto.SpriteBatchBackground);
    }

    private void DrawSkillAnimations()
    {
        _skillAnimationsList.ForEach(x => x.Draw());
    }

    private void DrawPausedShade()
    {
        var screenRectangle = new Rectangle(0, 0, GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize);
        GlobalVariablesDto.SpriteBatchInterface.Draw(GlobalVariablesDto.Pixel, screenRectangle, Color.Black * 0.4f);
    }

    private void DrawBattle()
    {
        _battleManager.GetAllUnits().ForEach(x => x.Draw());
    }

    private void DrawSelectionAreaComponent()
    {
        _selectionAreaComponent.DrawOnFocusedUnits(_focusedUnitsList, GlobalVariablesDto.SpriteBatchInterface);
    }

    #endregion

    #region Events

    #region Pause Menu Actions

    private void OnResumeAction()
    {
        TogglePauseFlag();
    }

    private void OnOptionsAction()
    {
        GlobalVariablesDto.PushScreen?.Invoke(ScreenConst.OptionScreen);
    }

    private void OnRestartAction()
    {
        base.Initialize();
        GlobalVariablesDto.ResetFollow(GlobalVariablesDto.SpriteBatchBackground);
        OnResumeAction();
    }

    private void OnMapAction()
    {
        BattleFinish(isGameOver: true);
    }

    #endregion

    #endregion
}

public class PositionableAnimation
{
    public List<BaseUnitEntity> Units { get; set; }
    public AnimationClip Animation { get; set; }

    public PositionableAnimation(List<BaseUnitEntity> units, AnimationClip animation)
    {
        Units = units;
        Animation = animation;
    }

    public void Update()
    {
        Animation.Update();
    }

    public void Draw()
    {
        foreach (var unit in Units)
            Animation.Draw(unit.Rectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchInterface);
    }
}
