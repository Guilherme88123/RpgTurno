using Domain.Const.Screen;
using Domain.Const.Version;
using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Enum.Transition;
using Domain.Interface.Cursor;
using Domain.Interface.Language;
using Domain.Interface.Screen;
using Domain.Interface.Transition;
using Domain.Model.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RpgTurno.Screen.Map.World.Stage;
using Service.Save;
using System;
using System.Threading.Tasks;

namespace RpgTurnoApp;

public class RpgTurno : Game
{
    public IScreenManager ScreenManager;
    public ITransitionManager TransitionManager;
    public ICursorManager CursorManager;

    public string InitialScreenCode = ScreenConst.MenuScreen;

    private int _frames;
    private float _fps;
    private double _elapsedTime;

    public RpgTurno()
    {
        LoadSettings();

        Window.Title = VersionConst.GameName;
        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = GlobalOptionsDto.RealWidthSize;
        graphics.PreferredBackBufferHeight = GlobalOptionsDto.RealHeightSize;
        graphics.HardwareModeSwitch = true;
        graphics.IsFullScreen = GlobalOptionsDto.Fullscreen;
        graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120d);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        graphics.ApplyChanges();

        GlobalVariablesDto.Graphics = graphics;
    }

    #region Settings

    private void LoadSettings()
    {
        LoadSettingsAsync().Wait();
    }

    private async Task LoadSettingsAsync()
    {
        var settings = await GetSettingsAsync();
        ApplySettings(settings);
    }

    private async Task<SettingsModel> GetSettingsAsync()
    {
        if (!await SaveManager.HasSettingsSaveAsync())
            await SaveManager.CreateDefaultSettingsAsync();

        return await SaveManager.GetSettingsSaveAsync();
    }

    private void ApplySettings(SettingsModel settings)
    {
        GlobalOptionsDto.RealWidthSize = settings.ResolutionWidth;
        GlobalOptionsDto.RealHeightSize = settings.ResolutionHeight;
        GlobalOptionsDto.Fullscreen = settings.Fullscreen;
        GlobalOptionsDto.MusicVolume = settings.MusicVolume;
        GlobalOptionsDto.EffectsVolume = settings.EffectsVolume;
        GlobalOptionsDto.Language = settings.Language;
        GlobalOptionsDto.ShowFps = settings.ShowFps;
    }

    #endregion

    protected override void Initialize()
    {
        ScreenManager = GlobalVariablesDto.GetService<IScreenManager>();
        TransitionManager = GlobalVariablesDto.GetService<ITransitionManager>();

        GlobalVariablesDto.Content = Content;
        GlobalVariablesDto.ChangeScreen = screenCode => TransitionManager.StartTransition(TransitionType.Fade, () => ScreenManager.ChangeScreen(screenCode));
        GlobalVariablesDto.PushScreen = screenCode => TransitionManager.StartTransition(TransitionType.Fade, () => ScreenManager.PushScreen(screenCode));
        GlobalVariablesDto.PopScreen = () => TransitionManager.StartTransition(TransitionType.Fade, ScreenManager.PopScreen);
        GlobalVariablesDto.Exit = Exit;

        CursorManager = GlobalVariablesDto.GetService<ICursorManager>();

        var languageService = GlobalVariablesDto.GetService<ILanguageService>();
        languageService.SetLanguage(GlobalOptionsDto.Language);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch spriteBatchBackground = new SpriteBatch(GraphicsDevice);
        SpriteBatch spriteBatchEntities = new SpriteBatch(GraphicsDevice);
        SpriteBatch spriteBatchInterface = new SpriteBatch(GraphicsDevice);
        SpriteBatch spriteBatchText = new SpriteBatch(GraphicsDevice);
        SpriteBatch spriteBatchTransition = new SpriteBatch(GraphicsDevice);

        Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);

        //GlobalVariablesDto.FontArial = Content.Load<SpriteFont>("Arial");
        //GlobalVariablesDto.FontThickPixels = Content.Load<SpriteFont>("ThickPixels");
        //GlobalVariablesDto.FontLazyFox = Content.Load<SpriteFont>("LazyFox");
        //GlobalVariablesDto.FontStacked = Content.Load<SpriteFont>("Stacked");
        //GlobalVariablesDto.FontManaRoot = Content.Load<SpriteFont>("ManaRoot");
        //GlobalVariablesDto.FontManaTrunk = Content.Load<SpriteFont>("ManaTrunk");
        GlobalVariablesDto.FontBadge = Content.Load<SpriteFont>("Badge");

        //GlobalVariablesDto.FontArial.Spacing = 2;
        //GlobalVariablesDto.FontThickPixels.Spacing = 2;
        //GlobalVariablesDto.FontLazyFox.Spacing = 2;
        //GlobalVariablesDto.FontStacked.Spacing = 2;
        //GlobalVariablesDto.FontManaRoot.Spacing = 2;
        //GlobalVariablesDto.FontManaTrunk.Spacing = 2;
        GlobalVariablesDto.FontBadge.Spacing = 2;

        GlobalVariablesDto.SpriteBatchBackground = spriteBatchBackground;
        GlobalVariablesDto.SpriteBatchEntities = spriteBatchEntities;
        GlobalVariablesDto.SpriteBatchInterface = spriteBatchInterface;
        GlobalVariablesDto.SpriteBatchText = spriteBatchText;
        GlobalVariablesDto.SpriteBatchTransition = spriteBatchTransition;
        GlobalVariablesDto.Pixel = pixel;

        MediaPlayer.Volume = GlobalOptionsDto.MusicVolumeFloat;
        MediaPlayer.IsRepeating = true;

        RunInitialScreen();
    }

    private void RunInitialScreen()
    {
        ScreenManager.ChangeScreen(InitialScreenCode);
    }

    protected override void Update(GameTime gameTime)
    {
        GlobalVariablesDto.GameTime = gameTime;
        GlobalVariablesDto.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        GlobalVariablesDto.AcumulatedDeltaTime += GlobalVariablesDto.DeltaTime;

        UpdateFpsCounter();

        base.Update(gameTime);

        TransitionManager.Update(gameTime);
        CursorManager.Update(gameTime);

        if (TransitionManager.IsTransitionRunning)
            return;

        CursorManager.BeginFrame();

        ScreenManager.ActualScreen.Update(gameTime);

        CursorManager.EndFrame();
    }

    private void UpdateFpsCounter()
    {
        _frames++;
        _elapsedTime += GlobalVariablesDto.DeltaTime;

        if (_elapsedTime >= 1.0) // Atualiza FPS a cada 1s
        {
            _fps = _frames / (float)_elapsedTime;
            _frames = 0;
            _elapsedTime = 0;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        var backgroundColor = new Color(71, 171, 169);
        GraphicsDevice.Clear(backgroundColor);

        var screenScaleMatrix = GetScreenScaleMatrix();

        GlobalVariablesDto.SpriteBatchBackground.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, transformMatrix: screenScaleMatrix);
        GlobalVariablesDto.SpriteBatchEntities.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, transformMatrix: screenScaleMatrix);
        GlobalVariablesDto.SpriteBatchInterface.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, transformMatrix: screenScaleMatrix);
        GlobalVariablesDto.SpriteBatchText.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, transformMatrix: screenScaleMatrix);
        GlobalVariablesDto.SpriteBatchTransition.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, transformMatrix: screenScaleMatrix);

        ScreenManager.ActualScreen.Draw();
        TransitionManager.Draw(GlobalVariablesDto.SpriteBatchTransition);
        CursorManager.Draw(GlobalVariablesDto.SpriteBatchTransition);

        if (GlobalOptionsDto.ShowFps)
            DrawFps();

        GlobalVariablesDto.SpriteBatchBackground.End();
        GlobalVariablesDto.SpriteBatchEntities.End();
        GlobalVariablesDto.SpriteBatchInterface.End();
        GlobalVariablesDto.SpriteBatchText.End();
        GlobalVariablesDto.SpriteBatchTransition.End();

        base.Draw(gameTime);
    }

    private Matrix GetScreenScaleMatrix()
    {
        float scaleX = (float)GraphicsDevice.Viewport.Width / GlobalOptionsDto.WidthSize;
        float scaleY = (float)GraphicsDevice.Viewport.Height / GlobalOptionsDto.HeightSize;

        return Matrix.CreateScale(scaleX, scaleY, 1f);
    }

    private void DrawFps()
    {
        string fpsText = $"FPS: {_fps:F0}";
        GlobalVariablesDto.SpriteBatchText.DrawString(GlobalVariablesDto.GlobalFont, fpsText, new Vector2(30, 30), Color.Black);
    }
}
