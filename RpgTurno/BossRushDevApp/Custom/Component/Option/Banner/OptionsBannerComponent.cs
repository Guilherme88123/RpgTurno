using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Banners;

namespace RpgTurno.Custom.Component.Option.Banner;

public class OptionsBannerComponent : FrameComponent
{
    private const int Margin = 64;

    private readonly ExitOptionsBannerComponent _exitButton = new();

    public OptionsBannerComponent()
    {
        AnimationManager.Add(true, new WoodBannerSprite());

        AddChild(_exitButton);

        Bounds = new(0, 0, 832, 960);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _exitButton.SetPosition(Bounds.Right - Margin - _exitButton.Bounds.Width, Bounds.Y + Margin);
    }
}
