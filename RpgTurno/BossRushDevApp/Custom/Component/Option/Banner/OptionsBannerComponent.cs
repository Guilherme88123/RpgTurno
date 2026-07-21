using Domain.Model.Components.Base;
using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Ribbons.Small;
using System.Drawing.Printing;

namespace RpgTurno.Custom.Component.Option.Banner;

public class OptionsBannerComponent : FrameComponent
{
    private const int Width = 832;
    private const int Height = 960;
    private const int Margin = 64;
    private static int ButtonWidth => Width - Margin * 6;

    private readonly TextComponent _titleText = new(positionXByCenter: true, positionYByCenter: true);
    private ImageComponent _titleBackground = new(new BlueSmallRibbonSprite(), ButtonWidth, 64);

    private readonly ExitOptionsBannerComponent _exitButton = new();
    private readonly RadioOptionsBannerComponent _musicRadio = new(ButtonWidth, "Music Volume");
    private readonly RadioOptionsBannerComponent _sfxRadio = new(ButtonWidth, "Effects Volume");
    private readonly SwitchOptionsBannerComponent _fullscreenSwitch = new(ButtonWidth, "Fullscreen");
    private readonly SwitchOptionsBannerComponent _fpsSwitch = new(ButtonWidth, "Show FPS");

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

        Bounds = new(0, 0, Width, Height);
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

        _exitButton.SetPosition(GetXMiddlePosition(_exitButton.Bounds.Width), Bounds.Bottom - Margin - _exitButton.Bounds.Height);
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
        return Bounds.Y + Margin + index * componentHeight;
    }
}
