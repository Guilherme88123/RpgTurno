using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Interface.Screen;
using Domain.Interface.UiManager;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RpgTurnoApp.Screen.Base;

public abstract class BaseScreen : IScreen
{
    public abstract string ScreenCode { get; }

    protected readonly GameSession GameSession = GlobalVariablesDto.GetService<GameSession>();

    private readonly IUiManagerService _componentsService;

    protected BaseScreen()
    {
        _componentsService = GlobalVariablesDto.GetService<IUiManagerService>();
    }

    #region Initialization

    public virtual void Initialize()
    {
        _componentsService.ClearComponents();
        _componentsService.AddComponent(InitializeComponents());
    }

    protected virtual List<BaseComponent> InitializeComponents()
    {
        return new();
    }

    #endregion

    #region Updating

    public virtual void Update(GameTime gameTime)
    {
        _componentsService.UpdateComponents(gameTime);

        GlobalVariablesDto.PreviousMouseDown = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;

        UpdateInputsState();
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
    }

    private void DrawComponents()
    {
        _componentsService.DrawComponents(GlobalVariablesDto.SpriteBatchInterface);
    }

    #endregion

    #region Exiting

    public virtual void Exit()
    {
    }

    #endregion
}
