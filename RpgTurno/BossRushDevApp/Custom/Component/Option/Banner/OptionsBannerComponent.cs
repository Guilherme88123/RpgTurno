using Domain.Dto.Components.Dropdown;
using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Ribbons.Small;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Custom.Component.Option.Banner;

public class OptionsBannerComponent : FrameComponent
{
    private const int Width = 832;
    private const int Height = 960;
    private const int Margin = 64;
    private const int Spacing = 16;

    private static int ButtonWidth => Width - Margin * 6 - 32;
    private static int ButtonHeight => Height / 10;

    private readonly TextComponent _titleText = new(positionXByCenter: true, positionYByCenter: true);
    private ImageComponent _titleBackground = new(new BlueSmallRibbonSprite(), ButtonWidth, 64);

    private readonly ExitOptionsBannerComponent _exitButton = new();
    private readonly RadioOptionsBannerComponent _musicRadio = new(ButtonWidth, ButtonHeight, "Music Volume", UpdateMusicVolume);
    private readonly RadioOptionsBannerComponent _sfxRadio = new(ButtonWidth, ButtonHeight, "Effects Volume", UpdateSfxVolume);
    private readonly SwitchOptionsBannerComponent _fullscreenSwitch = new(ButtonWidth, ButtonHeight, "Fullscreen", ToggleFullscreen);
    private readonly SwitchOptionsBannerComponent _fpsSwitch = new(ButtonWidth, ButtonHeight, "Show FPS", ToggleShowFps);
    private readonly DropdownOptionsBannerComponent _screenSizeDropdown = new(ButtonWidth, ButtonHeight, "Window Size", ToggleScreenSize, GetScreenSizeDropdownItens());

    public OptionsBannerComponent()
    {
        AnimationManager.Add(true, new WoodBannerSprite());

        _titleText.SetText("Options");

        AddChild(_titleBackground);
        AddChild(_titleText);
        AddChild(_exitButton);
        AddChild(_musicRadio);
        AddChild(_sfxRadio);
        AddChild(_fullscreenSwitch);
        AddChild(_fpsSwitch);
        AddChild(_screenSizeDropdown);

        Bounds = new(0, 0, Width, Height);

        InitializeValues();
    }

    private void InitializeValues()
    {
        _musicRadio.Value = GlobalOptionsDto.MusicVolume;
        _sfxRadio.Value = GlobalOptionsDto.SfxVolume;
        _fullscreenSwitch.Value = GlobalOptionsDto.Fullscreen;
        _fpsSwitch.Value = GlobalOptionsDto.ShowFps;
        _screenSizeDropdown.SelectedItemIndex = _screenSizeDropdown.ListItensDto.First(x =>
            ((Vector2)x.Value).X == GlobalOptionsDto.RealWidthSize &&
            ((Vector2)x.Value).Y == GlobalOptionsDto.RealHeightSize).Id;

        _fullscreenSwitch.ReloadText();
        _fpsSwitch.ReloadText();
        _screenSizeDropdown.ReloadText();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _exitButton.IsEnable = !_screenSizeDropdown.IsOpen;
        _exitButton.Text.IsVisible = !_screenSizeDropdown.IsOpen;
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

        _exitButton.SetPosition(GetXMiddlePosition(_exitButton.Bounds.Width), Bounds.Bottom - Margin - _exitButton.Bounds.Height - Spacing);
    }

    private void SetChildComponentPosition(BaseComponent component, int index)
    {
        component.SetPosition(GetXMiddlePosition(_musicRadio.Bounds.Width), GetYPositionByIndex(_musicRadio.Bounds.Height, index));
    }

    private int GetXMiddlePosition(int componentWidth)
    {
        return Bounds.Center.X - componentWidth / 2;
    }

    private int GetYPositionByIndex(int componentHeight, int index)
    {
        return Bounds.Y + Margin + index * componentHeight + Spacing * index - 1;
    }

    #region Button Actions

    public static void UpdateMusicVolume(int volume)
    {
        GlobalOptionsDto.MusicVolume = volume;
        MediaPlayer.Volume = GlobalOptionsDto.MusicVolumeFloat;
    }

    public static void UpdateSfxVolume(int volume)
    {
        GlobalOptionsDto.SfxVolume = volume;
    }

    public static void ToggleShowFps(bool showFps)
    {
        GlobalOptionsDto.ShowFps = showFps;
    }

    public static void ToggleFullscreen(bool isFullscreen)
    {
        GlobalVariablesDto.Graphics.IsFullScreen = isFullscreen;
        GlobalVariablesDto.Graphics.ApplyChanges();
        GlobalOptionsDto.Fullscreen = isFullscreen;
    }

    public static void ToggleScreenSize(DropdownItemDto dto)
    {
        var size = dto.Value as Vector2?;

        if (size is null) return;

        var width = (int)size.Value.X;
        var height = (int)size.Value.Y;

        GlobalOptionsDto.RealWidthSize = width;
        GlobalOptionsDto.RealHeightSize = height;
        GlobalVariablesDto.Graphics.PreferredBackBufferWidth = width;
        GlobalVariablesDto.Graphics.PreferredBackBufferHeight = height;
        GlobalVariablesDto.Graphics.ApplyChanges();
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

    #endregion
}
