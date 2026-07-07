using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum.Battle;
using Domain.Enum.Component.Cursor;
using Domain.Enum.Skill.Type;
using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Entity.Units.Base.Skill.Definition;
using Domain.Model.Skill.Base.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Custom.CustomComponents.Play.Background;
using RpgTurno.Custom.CustomComponents.Play.Banners;
using RpgTurno.Custom.CustomComponents.Play.DamageText;
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

    private readonly BattleManager _battleManager = new();

    private SelectionAreaComponent _selectionAreaComponent;
    private BaseUnitEntity _focusedEntity;
    private UnitBannerComponent _focusedUnitBannerComponent;

    private PlayBackgroundComponent _backgroundImageComponent;

    private TurnQueueComponent _turnQueueComponent;
    private CurrentUnitTurnIndicatorComponent _currentTurnUnitComponent;

    private readonly List<DamageTextComponent> _damagesTextList = new();

    private SkillSelectBannerComponent _skillSelectComponent;

    private WaveIndicatorComponent _waveIndicatorComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _battleManager.Initialize(GetAllies(), GameSession.CurrentStageCode);
        _battleManager.OnSkillExecute += AddSkillText;
        _battleManager.OnTurnStart += OnTurnStart;
        _battleManager.OnTurnFinish += OnTurnFinish;
        _battleManager.OnBattleFinish += BattleFinish;

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
        UpdateSkillTexts(gameTime);
        UpdateWaveIndicator();

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

    private void AddSkillText(BaseUnitEntity sender, List<BaseUnitEntity> targets, UnitSkill skill, int value)
    {
        var (valueText, color) = GetSkillStyleByType(skill.Type, value);

        if (string.IsNullOrEmpty(valueText))
            return;

        foreach (var target in targets)
            _damagesTextList.Add(new DamageTextComponent((int)target.Center.X, (int)target.Center.Y, valueText, color));
    }

    private (string, Color) GetSkillStyleByType(SkillType type, int value)
    {
        return type switch
        {
            SkillType.Attack => ($"-{value}", Color.Red),
            SkillType.Heal => ($"+{value}", Color.Green),
            _ => (string.Empty, Color.White),
        };
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

        OnHoverOutUnitAction();
    }

    private void OnHoverInUnitAction(BaseUnitEntity unit)
    {
        SetFocusedEntity(unit);
        SetHoverCursor();
    }

    private void OnHoverOutUnitAction()
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

    #region Skill Texts

    private void UpdateSkillTexts(GameTime gameTime)
    {
        _damagesTextList.ForEach(x => x.Update(gameTime));
        _damagesTextList.RemoveAll(x => x.IsDestroyed);
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
