using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Model.Components.Base;
using RpgTurno.Custom.Component.Menu.Button;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Menu;

public class MenuScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.MenuScreen;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        MenuButtonComponent startButton = new();
        MenuButtonComponent optionsButton = new();
        MenuButtonComponent creditsButton = new();
        MenuButtonComponent exitButton = new();

        startButton.SetText("Start");
        optionsButton.SetText("Options");
        creditsButton.SetText("Credits");
        exitButton.SetText("Exit");

        var initialPositionY = GlobalOptionsDto.HeightSize / 3;

        startButton.SetPositionWithIndex(initialPositionY, 0);
        optionsButton.SetPositionWithIndex(initialPositionY, 1);
        creditsButton.SetPositionWithIndex(initialPositionY, 2);
        exitButton.SetPositionWithIndex(initialPositionY, 3);

        startButton.Click = StartGame;
        optionsButton.Click = GoToOptionsScreen;
        exitButton.Click = ExitGame;

        return new()
        {
            startButton,
            optionsButton,
            creditsButton,
            exitButton,
        };
    }

    #region Buttons Methods

    private void StartGame()
    {
        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.MapScreen);
    }

    private void GoToOptionsScreen()
    {
        GlobalVariablesDto.PushScreen?.Invoke(ScreenConst.OptionScreen);
    }

    private void ExitGame()
    {
        GlobalVariablesDto.Exit?.Invoke();
    }

    #endregion

    #endregion
}
