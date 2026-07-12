using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Sprite;

namespace RpgTurno.Custom.Component.Map.Banner;

public class MapPauseBannerComponent : FrameComponent
{
    public MapPauseBannerComponent()
    {
        AnimationManager.Add(true, new SpecialPaperBannerSprite());

        Bounds = new(0, 0, 320, 640);
    }
}
