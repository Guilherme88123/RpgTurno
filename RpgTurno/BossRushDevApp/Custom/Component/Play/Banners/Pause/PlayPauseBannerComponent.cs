using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using System;

namespace RpgTurno.Custom.Component.Play.Banners.Pause;

public class PlayPauseBannerComponent : FrameComponent
{
    private const int Spacing = -16;
    private const int MarginTop = 80;
    private const int MarginBottom = 160;

    private readonly TextComponent _titleText = new(positionXByCenter: true, positionYByCenter: true);

    private readonly ButtonPlayPauseBannerComponent _resumeButton;
    private readonly ButtonPlayPauseBannerComponent _optionsButton;
    private readonly ButtonPlayPauseBannerComponent _restartButton;
    private readonly ButtonPlayPauseBannerComponent _mapButton;

    public PlayPauseBannerComponent(Action onResumeAction, Action onOptionsAction, Action onRestartAction, Action onMapAction)
    {
        AnimationManager.Add(true, new SpecialPaperBannerSprite());

        _titleText.SetText("Paused");
        _resumeButton = new("Resume", onResumeAction);
        _optionsButton = new("Options", onOptionsAction);
        _restartButton = new("Restart", onRestartAction, isDanger: true);
        _mapButton = new("Return To Map", onMapAction, isDanger: true);

        AddChild(_titleText);
        AddChild(_resumeButton);
        AddChild(_optionsButton);
        AddChild(_restartButton);
        AddChild(_mapButton);

        Bounds = new(0, 0, 534, 640);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _titleText.SetPosition(Bounds.X + Bounds.Width / 2, Bounds.Y + MarginTop);

        SetButtonPositionByIndex(_resumeButton, 3);
        SetButtonPositionByIndex(_optionsButton, 2);
        SetButtonPositionByIndex(_restartButton, 1);
        SetButtonPositionByIndex(_mapButton, 0);
    }

    private void SetButtonPositionByIndex(ButtonPlayPauseBannerComponent button, int index)
    {
        var positionX = Bounds.X + Bounds.Width / 2 - button.Bounds.Width / 2;
        var positionY = Bounds.Bottom - MarginBottom - (button.Bounds.Height + Spacing) * index;

        button.SetPosition(positionX, positionY);
    }
}
