using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Interface.Screen;
using Domain.Interface.UiManager;
using Domain.Model.Components.Base;
using Domain.Model.Components.Cursor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Custom.CustomComponents.Play.Cursor;
using System.Collections.Generic;

namespace RpgTurnoApp.Screen.Base;

public abstract class BaseScreen : IScreen
{
    public abstract string ScreenCode { get; }

    protected readonly GameSession GameSession = GlobalVariablesDto.GetService<GameSession>();
    protected CursorComponent CursorComponent;

    private readonly IUiManagerService _componentsService;

    protected BaseScreen()
    {
        _componentsService = GlobalVariablesDto.GetService<IUiManagerService>();
    }

    #region Initialization

    public virtual void Initialize()
    {
        _componentsService.AddComponent(InitializeComponents());
        InitializeCursor();
    }

    protected virtual List<BaseComponent> InitializeComponents()
    {
        return new();
    }

    private void InitializeCursor()
    {
        CursorComponent = new CustomCursorComponent();
    }

    #endregion

    #region Updating

    public virtual void Update(GameTime gameTime)
    {
        _componentsService.UpdateComponents(gameTime);

        GlobalVariablesDto.PreviousMouseDown = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;

        UpdateCursor(gameTime);

        UpdateInputsState();
    }

    private void UpdateInputsState()
    {
        GlobalVariablesDto.KeyboardState = Keyboard.GetState();
        GlobalVariablesDto.MouseState = Mouse.GetState();
    }

    private void UpdateCursor(GameTime gameTime)
    {
        CursorComponent.Update(gameTime);
    }

    #endregion

    #region Drawing

    public virtual void Draw()
    {
        DrawComponents();
        DrawCursor();
    }

    private void DrawComponents()
    {
        _componentsService.DrawComponents(GlobalVariablesDto.SpriteBatchInterface);
    }

    private void DrawCursor()
    {
        CursorComponent.Draw(GlobalVariablesDto.SpriteBatchInterface);
    }

    #endregion

    #region Exiting

    public virtual void Exit()
    {
    }

    #endregion
}
