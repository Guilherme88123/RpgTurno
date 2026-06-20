using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Interface.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RpgTurnoApp;

public class RpgTurno : Game
{
    public IScreenManager ScreenManager;

    public string InitialScreenCode = ScreenConst.PlayScreen;

    private int _frames;
    private float _fps;
    private double _elapsedTime;

    public RpgTurno()
    {
        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = GlobalOptionsDto.WidthSize;
        graphics.PreferredBackBufferHeight = GlobalOptionsDto.HeightSize;
        graphics.HardwareModeSwitch = false;
        graphics.IsFullScreen = GlobalOptionsDto.Fullscreen;
        graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120d);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        graphics.ApplyChanges();

        GlobalVariablesDto.Graphics = graphics;
    }

    protected override void Initialize()
    {
        ScreenManager = GlobalVariablesDto.GetService<IScreenManager>();

        GlobalVariablesDto.Content = Content;
        GlobalVariablesDto.ChangeScreen = ScreenManager.ChangeScreen;
        GlobalVariablesDto.PushScreen = ScreenManager.PushScreen;
        GlobalVariablesDto.PopScreen = ScreenManager.PopScreen;
        GlobalVariablesDto.Exit = Exit;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch spriteBatchBackground = new SpriteBatch(GraphicsDevice);
        SpriteBatch spriteBatchEntities = new SpriteBatch(GraphicsDevice);
        SpriteBatch spriteBatchInterface = new SpriteBatch(GraphicsDevice);

        Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);

        GlobalVariablesDto.FontArial = Content.Load<SpriteFont>("Arial");
        GlobalVariablesDto.FontThickPixels = Content.Load<SpriteFont>("ThickPixels");
        GlobalVariablesDto.FontLazyFox = Content.Load<SpriteFont>("LazyFox");

        GlobalVariablesDto.SpriteBatchBackground = spriteBatchBackground;
        GlobalVariablesDto.SpriteBatchEntities = spriteBatchEntities;
        GlobalVariablesDto.SpriteBatchInterface = spriteBatchInterface;
        GlobalVariablesDto.Pixel = pixel;

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

        ScreenManager.ActualScreen.Update(gameTime);

        UpdateFpsCounter();

        base.Update(gameTime);
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

        GlobalVariablesDto.SpriteBatchBackground.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
        GlobalVariablesDto.SpriteBatchEntities.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
        GlobalVariablesDto.SpriteBatchInterface.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

        ScreenManager.ActualScreen.Draw();
        DrawFps();

        GlobalVariablesDto.SpriteBatchBackground.End();
        GlobalVariablesDto.SpriteBatchEntities.End();
        GlobalVariablesDto.SpriteBatchInterface.End();

        base.Draw(gameTime);
    }

    private void DrawFps()
    {
        string fpsText = $"FPS: {_fps:F0}";
        GlobalVariablesDto.SpriteBatchInterface.DrawString(GlobalVariablesDto.FontThickPixels, fpsText, new Vector2(10, 10), Color.Black);
    }
}
