using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Sprite;
using System;

namespace RpgTurno.Custom.Component.Map.Banner;

public class MapPauseBannerComponent : FrameComponent
{
    private const int Spacing = -16;
    private const int MarginTop = 80;
    private const int MarginBottom = 160;

    private readonly TextComponent _titleText = new(positionXByCenter: true, positionYByCenter: true);

    private readonly ButtonMapPauseBannerComponent _resumeButton;
    private readonly ButtonMapPauseBannerComponent _optionsButton;
    private readonly ButtonMapPauseBannerComponent _menuButton;
    private readonly ButtonMapPauseBannerComponent _exitButton;

    public MapPauseBannerComponent(Action onResumeAction, Action onOptionsAction, Action onMenuAction, Action onExitAction)
    {
        AnimationManager.Add(true, new SpecialPaperBannerSprite());

        _titleText.SetText("Paused");
        _resumeButton = new("Resume", onResumeAction);
        _optionsButton = new("Options", onOptionsAction);
        _menuButton = new("Main Menu", onMenuAction);
        _exitButton = new("Exit", onExitAction, isDanger: true);

        AddChild(_titleText);
        AddChild(_resumeButton);
        AddChild(_optionsButton);
        AddChild(_menuButton);
        AddChild(_exitButton);

        Bounds = new(0, 0, 320, 640);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _titleText.SetPosition(Bounds.X + Bounds.Width / 2, Bounds.Y + MarginTop);

        SetButtonPositionByIndex(_resumeButton, 3);
        SetButtonPositionByIndex(_optionsButton, 2);
        SetButtonPositionByIndex(_menuButton, 1);
        SetButtonPositionByIndex(_exitButton, 0);
    }

    private void SetButtonPositionByIndex(ButtonMapPauseBannerComponent button, int index)
    {
        var positionX = Bounds.X + Bounds.Width / 2 - button.Bounds.Width / 2;
        var positionY = Bounds.Bottom - MarginBottom - (button.Bounds.Height + Spacing) * index;

        button.SetPosition(positionX, positionY);
    }
}
