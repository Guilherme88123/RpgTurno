using Domain.Const.Screen;
using Domain.Const.Sound.Music;
using Domain.Dto.Global;
using Domain.Enum.Stage;
using Domain.Application.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RpgTurno.Custom.Component.Map.Banner.Finish;
using RpgTurno.Custom.Component.Map.Banner.Pause;
using RpgTurno.Custom.CustomComponents.Map.AlliesParty;
using RpgTurno.Custom.CustomComponents.Map.Background;
using RpgTurno.Custom.CustomComponents.Map.Stage;
using RpgTurno.Screen.Map.World;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using RpgTurno.Custom.Component.Map.Button;
using Domain.Dto.Map;

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

    private bool _isFinished;
    private GameFinishBannerComponent _finishBannerComponent;

    private NextBattleButtonComponent _nextButtonComponent;
    private PreviousBattleButtonComponent _previousButtonComponent;
    private StartBattleButtonComponent _startButtonComponent;

    #region Initialize

    public override void Initialize()
    {
        base.Initialize();

        _isFinished = false;
        _isPaused = false;
    }

    protected override List<BaseComponent> InitializeComponents()
    {
        _worldManager = new();
        _worldManager.OnPlayScreenEntry += OnPlayScreenEntry;
        _worldManager.Initialize(GameSession.Save.Map);
        GameSession.OnStageCleared += _worldManager.OnStageCleared;

        _nodeBannerComponent = new();
        _nodeBannerComponent.IsVisible = false;

        _alliesPartyComponent = new();
        _alliesPartyComponent.SetAlliesParty(GameSession.Save.Allies);

        _backgroundImageComponent = new();

        _pauseBannerComponent = new(
            onResumeAction: OnResumeAction,
            onOptionsAction: OnOptionsAction,
            onMenuAction: OnMenuAction,
            onExitAction: OnExitAction);
        _pauseBannerComponent.IsVisible = false;
        _pauseBannerComponent.IsEnable = false;
        _pauseBannerComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _pauseBannerComponent.Bounds.Width / 2,
            GlobalOptionsDto.HeightSize / 2 - _pauseBannerComponent.Bounds.Height / 2);

        _startButtonComponent = new(_worldManager.TryEnterMapNode);
        _startButtonComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _startButtonComponent.Bounds.Width / 2,
            GlobalOptionsDto.HeightSize - _startButtonComponent.Bounds.Height - 48);

        _nextButtonComponent = new(_worldManager.TryWalkToNextNode);
        _nextButtonComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _nextButtonComponent.Bounds.Width / 2 + _startButtonComponent.Bounds.Width,
            GlobalOptionsDto.HeightSize - _nextButtonComponent.Bounds.Height - 48);

        _previousButtonComponent = new(_worldManager.TryWalkToPreviousNode);
        _previousButtonComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _previousButtonComponent.Bounds.Width / 2 - _startButtonComponent.Bounds.Width,
            GlobalOptionsDto.HeightSize - _previousButtonComponent.Bounds.Height - 48);

        _finishBannerComponent = new(onMenuAction: OnMenuAction);
        _finishBannerComponent.IsVisible = false;
        _finishBannerComponent.IsEnable = false;
        _finishBannerComponent.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _finishBannerComponent.Bounds.Width / 2,
            GlobalOptionsDto.HeightSize / 2 - _finishBannerComponent.Bounds.Height / 2);

        return new()
        {
            _nodeBannerComponent,
            _alliesPartyComponent,
            _startButtonComponent,
            _previousButtonComponent,
            _nextButtonComponent,
            _finishBannerComponent,
            _pauseBannerComponent,
        };
    }

    #endregion

    #region Navigation

    public override void OnGoTo(string originScreenCode)
    {
        if (originScreenCode == ScreenConst.OptionScreen)
            return;

        PlayMusic();

        if (originScreenCode != ScreenConst.PlayScreen)
            return;

        VerifyGameFinish();
    }

    private void PlayMusic()
    {
        MediaPlayer.Play(GlobalVariablesDto.Content.Load<Song>(MusicConst.MapMusic));
    }

    private void VerifyGameFinish()
    {
        if (!_worldManager.Map.Cleared)
            return;

        _isFinished = true;

        UpdateComponentsVisibility();
    }

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        VerifyPause();

        if (_isPaused || _isFinished)
            return;

        _worldManager.Update();

        UpdateNodeBanner();
        UpdateAlliesParty();
        UpdateButtons();
        UpdateBackground();
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

    private void UpdateButtons()
    {
        bool canEnterStage = _worldManager.CanPlayerEnterAtStage();
        bool canGoToNext = _worldManager.IsAbleToWalkToNext();
        bool canGoToPrevious = _worldManager.IsAbleToWalkToPrevious();

        _startButtonComponent.IsVisible = canEnterStage;
        _startButtonComponent.IsEnable = canEnterStage;

        _nextButtonComponent.IsVisible = canGoToNext;
        _nextButtonComponent.IsEnable = canGoToNext;

        _previousButtonComponent.IsVisible = canGoToPrevious;
        _previousButtonComponent.IsEnable = canGoToPrevious;
    }

    private void UpdateBackground()
    {
        _backgroundImageComponent.Update(GlobalVariablesDto.GameTime);
    }

    private void UpdateComponentsVisibility()
    {
        _pauseBannerComponent.IsVisible = _isPaused;
        _pauseBannerComponent.IsEnable = _isPaused;

        _finishBannerComponent.IsVisible = _isFinished;
        _finishBannerComponent.IsEnable = _isFinished;

        _alliesPartyComponent.IsEnable = !_isPaused;

        _nodeBannerComponent.IsVisible = !_isPaused && !_isFinished;
        _nodeBannerComponent.IsEnable = !_isPaused && !_isFinished;

        _startButtonComponent.IsVisible = !_isPaused && !_isFinished;
        _startButtonComponent.IsEnable = !_isPaused && !_isFinished;

        _nextButtonComponent.IsVisible = !_isPaused && !_isFinished;
        _nextButtonComponent.IsEnable = !_isPaused && !_isFinished;

        _previousButtonComponent.IsVisible = !_isPaused && !_isFinished;
        _previousButtonComponent.IsEnable = !_isPaused && !_isFinished;
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
        UpdateComponentsVisibility();
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
        GlobalVariablesDto.SpriteBatchInterface.Draw(GlobalVariablesDto.Pixel, screenRectangle, Color.Black * 0.4f);
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
        if (_isFinished)
            _ = ResetFinished();

        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.MenuScreen);
    }

    private async Task ResetFinished()
    {
        await Task.Delay(300);

        _isFinished = false;
    }

    private void OnExitAction()
    {
        GlobalVariablesDto.Exit?.Invoke();
    }

    #endregion

    #endregion
}
