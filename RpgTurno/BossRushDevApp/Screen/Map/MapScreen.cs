using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum.Stage;
using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Ally.Archer;
using Domain.Model.Entity.Units.Ally.Cleric;
using Domain.Model.Entity.Units.Ally.Lancer;
using Domain.Model.Entity.Units.Ally.Warrior;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Custom.Component.Map.Banner;
using RpgTurno.Custom.CustomComponents.Map.AlliesParty;
using RpgTurno.Custom.CustomComponents.Map.Background;
using RpgTurno.Custom.CustomComponents.Map.Stage;
using RpgTurno.Screen.Map.World;
using RpgTurno.Screen.Map.World.Stage.Node;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Map;

public class MapScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.MapScreen;

    private WorldManager _worldManager;

    private KeyboardState _previousKeyboardState;
    private bool _isPaused;
    private Keys _pauseKey = Keys.Escape;

    private WorldMapBackgroundComponent _backgroundImageComponent;
    private AlliesPartyComponent _alliesPartyComponent;
    private MapNodeBannerComponent _nodeBannerComponent;
    private MapPauseBannerComponent _pauseBannerComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _worldManager = new();
        _worldManager.OnPlayScreenEntry += OnPlayScreenEntry;
        _worldManager.Initialize();
        GameSession.OnStageCleared += _worldManager.OnStageCleared;

        _nodeBannerComponent = new();
        _nodeBannerComponent.IsVisible = false;

        _alliesPartyComponent = new();
        _alliesPartyComponent.SetAlliesParty(GameSession.Allies);

        _backgroundImageComponent = new();

        _pauseBannerComponent = new(
            onResumeAction: OnResumeAction,
            onOptionsAction: OnOptionsAction,
            onMenuAction: OnMenuAction,
            onExitAction: OnExitAction);
        _pauseBannerComponent.IsVisible = false;
        _pauseBannerComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _pauseBannerComponent.Bounds.Width / 2,
            GlobalOptionsDto.HeightSize / 2 - _pauseBannerComponent.Bounds.Height / 2);

        return new()
        {
            _nodeBannerComponent,
            _alliesPartyComponent,
            _pauseBannerComponent,
        };
    }

    public override void Initialize()
    {
        InitializeAllies();

        base.Initialize();
    }

    private void InitializeAllies()
    {
        List<BaseUnitEntity> allies =
        [
            new WarriorEntity(),
            new ArcherEntity(),
            new LancerEntity(),
            new ClericEntity(),
        ];

        GameSession.Allies = allies;
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

        _worldManager.Update();

        UpdateNodeBanner();
        UpdateAlliesParty();
    }

    private void UpdateNodeBanner()
    {
        if (_worldManager.Player.CurrentNode is StageMapNode stageNode && !_worldManager.Player.IsMoving)
        {
            _nodeBannerComponent.SetCurrentMapNode(stageNode);
            _nodeBannerComponent.IsVisible = true;
        }
        else
        {
            _nodeBannerComponent.IsVisible = false;
        }
    }

    private void UpdateAlliesParty()
    {
        _alliesPartyComponent.SetPositionByPlayer(_worldManager.Player, GameSession.IsInBattle);
    }

    private void UpdatePauseFlag()
    {
        _pauseBannerComponent.IsVisible = _isPaused;
        _pauseBannerComponent.IsEnable = _isPaused;

        _alliesPartyComponent.IsEnable = !_isPaused;

        _nodeBannerComponent.IsVisible = !_isPaused;
        _nodeBannerComponent.IsEnable = !_isPaused;
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

    #region Draw

    public override void Draw()
    {
        DrawBackground();

        if (_isPaused)
           DrawPausedShade();

        base.Draw();
    }

    private void DrawBackground()
    {
        _backgroundImageComponent.Draw(GlobalVariablesDto.SpriteBatchBackground);
    }

    private void DrawPausedShade()
    {
        var screenRectangle = new Rectangle(0, 0, GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize);
        GlobalVariablesDto.SpriteBatchInterface.Draw(GlobalVariablesDto.Pixel, screenRectangle, Color.Black * 0.3f);
    }

    #endregion

    #region Events

    #region Stage Selected

    private void OnPlayScreenEntry(StageCode stageCode)
    {
        GameSession.CurrentStageCode = stageCode;
        GameSession.IsInBattle = true;
    }

    #endregion

    #region Pause Menu Actions

    private void OnResumeAction()
    {
        TogglePauseFlag();
    }

    private void OnOptionsAction()
    {
        GlobalVariablesDto.PushScreen?.Invoke(ScreenConst.OptionScreen);
    }

    private void OnMenuAction()
    {
        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.MenuScreen);
    }

    private void OnExitAction()
    {
        GlobalVariablesDto.Exit?.Invoke();
    }

    #endregion

    #endregion
}
