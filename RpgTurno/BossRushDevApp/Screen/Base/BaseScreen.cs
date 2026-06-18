using Application.Model.MenuElements.Base;
using Domain.Dto.Global;
using Domain.Interface.Screen;
using Domain.Interface.UiManager;
using Domain.Model.Components.Custom.Cursor;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RpgTurnoApp.Screen.Base;

public abstract class BaseScreen : IScreen
{
    public abstract string ScreenCode { get; }

    private readonly IUiManagerService _componentsService;

    protected CursorComponent CursorComponent;

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
        CursorComponent = new CursorComponent();
    }

    #endregion

    #region Updating

    public virtual void Update()
    {
        UpdateInputsState();

        _componentsService.UpdateComponents();

        GlobalVariablesDto.IsMouseDown = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;
    }

    private void UpdateInputsState()
    {
        GlobalVariablesDto.KeyboardState = Keyboard.GetState();
        GlobalVariablesDto.MouseState = Mouse.GetState();
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
        _componentsService.DrawComponents();
    }

    private void DrawCursor()
    {
        CursorComponent.Draw();
    }

    #endregion

    #region Exiting

    public virtual void Exit()
    {
    }

    #endregion
}
