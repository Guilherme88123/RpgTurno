using Domain.Const.Screen;
using Domain.Const.Version;
using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Components.Text;
using RpgTurno.Custom.Component.Menu.Button;
using RpgTurno.Custom.Component.Menu.Logo;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Menu;

public class MenuScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.MenuScreen;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        MenuLogoComponent logo = new();
        MenuButtonComponent startButton = new();
        MenuButtonComponent optionsButton = new();
        MenuButtonComponent creditsButton = new();
        MenuButtonComponent exitButton = new(isDanger: true);
        TextComponent versionText = new();
        TextComponent creatorText = new();

        startButton.SetText("Start");
        optionsButton.SetText("Options");
        creditsButton.SetText("Credits");
        exitButton.SetText("Exit");
        versionText.SetText($"Version: {VersionConst.Version}");
        creatorText.SetText($"Por: Guilherme Doerner de Oliveira");

        var initialPositionY = GlobalOptionsDto.HeightSize / 3;

        logo.SetPosition(GlobalOptionsDto.WidthSize / 2 - logo.Bounds.Width / 2, 128);
        startButton.SetPositionWithIndex(initialPositionY, 1);
        optionsButton.SetPositionWithIndex(initialPositionY, 2);
        creditsButton.SetPositionWithIndex(initialPositionY, 2);
        exitButton.SetPositionWithIndex(initialPositionY, 3);
        versionText.SetPosition(30, GlobalOptionsDto.HeightSize - versionText.Bounds.Height - 30);
        creatorText.SetPosition(GlobalOptionsDto.WidthSize - creatorText.Bounds.Width - 30, GlobalOptionsDto.HeightSize - creatorText.Bounds.Height - 30);

        startButton.Click = StartGame;
        optionsButton.Click = GoToOptionsScreen;
        //creditsButton.Click = GoToCreditsScreen;
        exitButton.Click = ExitGame;

        return new()
        {
            logo,
            startButton,
            optionsButton,
            //creditsButton,
            exitButton,
            versionText,
            creatorText,
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
