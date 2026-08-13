using Domain.Const.Text;
using Domain.Dto.Components.Dropdown;
using Domain.Dto.Global;
using Domain.Dto.Language;
using Domain.Enum.Language;
using Domain.Interface.Language;
using Domain.Application.Components.Base;
using Domain.Application.Components.Image;
using Domain.Application.Components.Text;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Ribbons.Small;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Settings;
using Service.Save;
using System.Runtime.CompilerServices;
using System;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Application.Texture.Sprite.Custom.Ui.Flags;

namespace RpgTurno.Custom.Component.Option.Banner;

public class OptionsBannerComponent : FrameComponent
{
    private const int Width = 1120;
    private const int Height = 960;
    private const int Margin = 64;
    private const int Spacing = 16;

    private static int TitleBackgroundWidth => Width - Margin * 4 - 32;

    private static int ButtonWidth => Width / 2 - Margin * 2;
    private static int ButtonHeight => Height / 10;

    private readonly TextComponent _titleText = new(positionXByCenter: true, positionYByCenter: true);
    private ImageComponent _titleBackground = new(new BlueSmallRibbonSprite(), TitleBackgroundWidth, Margin);

    private readonly ExitOptionsBannerComponent _exitButton;
    private readonly RadioOptionsBannerComponent _musicRadio;
    private readonly RadioOptionsBannerComponent _sfxRadio;
    private readonly SwitchOptionsBannerComponent _fullscreenSwitch;
    private readonly SwitchOptionsBannerComponent _fpsSwitch;
    private readonly DropdownOptionsBannerComponent _screenSizeDropdown;
    private readonly DropdownOptionsBannerComponent _languageDropdown;

    public OptionsBannerComponent()
    {
        AnimationManager.Add(true, new WoodBannerSprite());

        _exitButton = new();
        _musicRadio = new(ButtonWidth, ButtonHeight, LanguageManager.Get(TextConst.MusicVolume), UpdateMusicVolume);
        _sfxRadio = new(ButtonWidth, ButtonHeight, LanguageManager.Get(TextConst.EffectsVolume), UpdateSfxVolume);
        _fullscreenSwitch = new(ButtonWidth, ButtonHeight, LanguageManager.Get(TextConst.Fullscreen), ToggleFullscreen);
        _fpsSwitch = new(ButtonWidth, ButtonHeight, LanguageManager.Get(TextConst.ShowFps), ToggleShowFps);
        _screenSizeDropdown = new(ButtonWidth, ButtonHeight, LanguageManager.Get(TextConst.WindowSize), ToggleScreenSize, GetScreenSizeDropdownItens());
        _languageDropdown = new(ButtonWidth, ButtonHeight, LanguageManager.Get(TextConst.Language), ToggleLanguage, GetLanguageDropdownItens());


        _titleText.SetText(LanguageManager.Get(TextConst.Options));

        AddChild(_titleBackground);
        AddChild(_titleText);
        AddChild(_exitButton);
        AddChild(_musicRadio);
        AddChild(_sfxRadio);
        AddChild(_fullscreenSwitch);
        AddChild(_fpsSwitch);
        AddChild(_screenSizeDropdown);
        AddChild(_languageDropdown);

        Bounds = new(0, 0, Width, Height);

        InitializeValues();
    }

    private void InitializeValues()
    {
        _musicRadio.Value = GlobalOptionsDto.MusicVolume;
        _sfxRadio.Value = GlobalOptionsDto.EffectsVolume;
        _fullscreenSwitch.Value = GlobalOptionsDto.Fullscreen;
        _fpsSwitch.Value = GlobalOptionsDto.ShowFps;
        _screenSizeDropdown.SelectedItemIndex = _screenSizeDropdown.ListItensDto.First(x =>
            ((Vector2)x.Value).X == GlobalOptionsDto.RealWidthSize &&
            ((Vector2)x.Value).Y == GlobalOptionsDto.RealHeightSize).Id;
        _languageDropdown.SelectedItemIndex = _languageDropdown.ListItensDto.First(x =>
            (LanguageType)x.Value == GlobalOptionsDto.Language).Id;

        _fullscreenSwitch.ReloadText();
        _fpsSwitch.ReloadText();
        _screenSizeDropdown.ReloadText();
        _languageDropdown.ReloadText();
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _titleText.SetPosition(Bounds.Center.X, Bounds.Y + Margin * 2);
        _titleBackground.SetPosition(GetXMiddlePosition(_titleBackground.Bounds.Width), Bounds.Y + Margin * 2 - 32);

        SetChildComponentPosition(_musicRadio, 1);
        SetChildComponentPosition(_sfxRadio, 2);
        SetChildComponentPosition(_fullscreenSwitch, 3);
        SetChildComponentPosition(_fpsSwitch, 4);
        SetChildComponentPosition(_screenSizeDropdown, 5);
        SetChildComponentPosition(_languageDropdown, 6);

        _exitButton.SetPosition(GetXMiddlePosition(_exitButton.Bounds.Width), Bounds.Bottom - Margin - _exitButton.Bounds.Height - Spacing);
    }

    private void SetChildComponentPosition(BaseComponent component, int index)
    {
        component.SetPosition(GetXPositionPosition(_musicRadio.Bounds.Width, index), GetYPositionByIndex(_musicRadio.Bounds.Height, index));
    }

    private int GetXMiddlePosition(int componentWidth)
    {
        return Bounds.Center.X - componentWidth / 2;
    }

    private int GetXPositionPosition(int componentWidth, int index)
    {
        var buttonGap = index % 2 == 0 ? Spacing : -(componentWidth + Spacing);

        return Bounds.Center.X + buttonGap;
    }

    private int GetYPositionByIndex(int componentHeight, int index)
    {
        var realYIndex = (index + 1) / 2;

        return Bounds.Y + Margin * 2 + realYIndex * componentHeight + Spacing * realYIndex - 1;
    }

    #region Button Actions

    public static void UpdateMusicVolume(int volume)
    {
        GlobalOptionsDto.MusicVolume = volume;
        MediaPlayer.Volume = GlobalOptionsDto.MusicVolumeFloat;

        SaveSettings();
    }

    public static void UpdateSfxVolume(int volume)
    {
        GlobalOptionsDto.EffectsVolume = volume;

        SaveSettings();
    }

    public static void ToggleShowFps(bool showFps)
    {
        GlobalOptionsDto.ShowFps = showFps;

        SaveSettings();
    }

    public static void ToggleFullscreen(bool isFullscreen)
    {
        GlobalVariablesDto.Graphics.IsFullScreen = isFullscreen;
        GlobalVariablesDto.Graphics.ApplyChanges();
        GlobalOptionsDto.Fullscreen = isFullscreen;

        SaveSettings();
    }

    public static void ToggleScreenSize(DropdownItemDto dto)
    {
        var size = dto.Value as Vector2?;

        if (size is null) 
            return;

        var width = (int)size.Value.X;
        var height = (int)size.Value.Y;

        GlobalOptionsDto.RealWidthSize = width;
        GlobalOptionsDto.RealHeightSize = height;
        GlobalVariablesDto.Graphics.PreferredBackBufferWidth = width;
        GlobalVariablesDto.Graphics.PreferredBackBufferHeight = height;
        GlobalVariablesDto.Graphics.ApplyChanges();

        SaveSettings();
    }

    public static List<DropdownItemDto> GetScreenSizeDropdownItens()
    {
        return new List<DropdownItemDto>()
            {
                new() { Id = 0, Text = "960x540", Value = new Vector2(960, 540) },
                new() { Id = 1, Text = "1280x720", Value = new Vector2(1280, 720) },
                new() { Id = 2, Text = "1600x900", Value = new Vector2(1600, 900) },
                new() { Id = 3, Text = "1920x1080", Value = new Vector2(1920, 1080) },
            };
    }

    public void ToggleLanguage(DropdownItemDto dto)
    {
        var language = dto.Value as LanguageType?;

        if (language is null) 
            return;

        GlobalOptionsDto.Language = language.Value;
        var languageService = GlobalVariablesDto.GetService<ILanguageService>();
        languageService.SetLanguage(GlobalOptionsDto.Language);

        GlobalVariablesDto.ReloadScreens?.Invoke();

        SaveSettings();
    }

    public static List<DropdownItemDto> GetLanguageDropdownItens()
    {
        return new List<DropdownItemDto>()
            {
                new() { Id = 0, Text = LanguageManager.Get(TextConst.English), Value = LanguageType.English, Icon = new EnglishFlagSprite() },
                new() { Id = 1, Text = LanguageManager.Get(TextConst.Portuguese), Value = LanguageType.Portuguese, Icon = new PortugueseFlagSprite() },
                new() { Id = 2, Text = LanguageManager.Get(TextConst.Spanish), Value = LanguageType.Spanish, Icon = new SpanishFlagSprite() },
            };
    }

    #endregion

    #region Settings Save Update

    private static void SaveSettings()
    {
        _ = SaveSettingsAsync();
    }

    private static async Task SaveSettingsAsync()
    {
        var settings = GetSettingsModel();
        _ = SaveManager.UpdateSettingsSaveAsync(settings);
    }

    private static SettingsModel GetSettingsModel()
    {
        return new SettingsModel()
        {
            MusicVolume = GlobalOptionsDto.MusicVolume,
            EffectsVolume = GlobalOptionsDto.EffectsVolume,
            Fullscreen = GlobalOptionsDto.Fullscreen,
            ShowFps = GlobalOptionsDto.ShowFps,
            ResolutionWidth = GlobalOptionsDto.RealWidthSize,
            ResolutionHeight = GlobalOptionsDto.RealHeightSize,
            Language = GlobalOptionsDto.Language,
        };
    }

    #endregion
}
