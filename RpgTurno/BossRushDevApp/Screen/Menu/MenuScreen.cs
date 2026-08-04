using Domain.Const.Screen;
using Domain.Const.Sound.Music;
using Domain.Const.Text;
using Domain.Const.Version;
using Domain.Dto.Global;
using Domain.Dto.Language;
using Domain.Application.Components.Base;
using Domain.Application.Components.Text;
using Microsoft.Xna.Framework.Media;
using RpgTurno.Custom.Component.Menu.Background;
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
        MenuBackgroundComponent background = new();
        MenuLogoComponent logo = new();
        MenuButtonComponent startButton = new();
        MenuButtonComponent optionsButton = new();
        MenuButtonComponent creditsButton = new();
        MenuButtonComponent exitButton = new(isDanger: true);
        TextComponent versionText = new();
        TextComponent creatorText = new();

        startButton.SetText(LanguageManager.Get(TextConst.Start));
        optionsButton.SetText(LanguageManager.Get(TextConst.Options));
        creditsButton.SetText(LanguageManager.Get(TextConst.Credits));
        exitButton.SetText(LanguageManager.Get(TextConst.Exit));
        versionText.SetText($"{LanguageManager.Get(TextConst.Version)}: {VersionConst.Version}");
        creatorText.SetText($"{LanguageManager.Get(TextConst.By)}: {VersionConst.GameOwner}");

        var initialPositionY = GlobalOptionsDto.HeightSize / 3 + 128;

        logo.SetPosition(GlobalOptionsDto.WidthSize / 2 - logo.Bounds.Width / 2, 128);
        startButton.SetPositionWithIndex(initialPositionY, 1);
        optionsButton.SetPositionWithIndex(initialPositionY, 2);
        creditsButton.SetPositionWithIndex(initialPositionY, 2);
        exitButton.SetPositionWithIndex(initialPositionY, 3);
        versionText.SetPosition(30, GlobalOptionsDto.HeightSize - versionText.Bounds.Height - 20);
        creatorText.SetPosition(GlobalOptionsDto.WidthSize - creatorText.Bounds.Width - 30, GlobalOptionsDto.HeightSize - creatorText.Bounds.Height - 20);

        startButton.Click = StartGame;
        optionsButton.Click = GoToOptionsScreen;
        //creditsButton.ValueUpdate = GoToCreditsScreen;
        exitButton.Click = ExitGame;

        return new()
        {
            background,
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
        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.SaveScreen);
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

    #region Navigation

    public override void OnGoTo(string originScreenCode)
    {
        if (originScreenCode == ScreenConst.OptionScreen)
            return;

        MediaPlayer.Play(GlobalVariablesDto.Content.Load<Song>(MusicConst.MenuMusic));
    }

    #endregion
}
