using Application.Model.MenuElements.Radio;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Bars;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

namespace RpgTurno.Custom.Component.Option.Banner;

public class RadioOptionsBannerComponent : RadioComponent
{
    public RadioOptionsBannerComponent(int width, string text)
    {
        AnimationManager.Add(true, new BlueButtonRegularSprite());
        DotSprite = new SquareBannerSprite();
        LineAnimation = new SmallBarBaseSprite();

        Bounds = new(0, 0, width, 128);

        SetText(text);
    }
}
