using Application.Model.MenuElements.Radio;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Bars;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;
using System;

namespace RpgTurno.Custom.Component.Option.Banner;

public class RadioOptionsBannerComponent : RadioComponent
{
    public RadioOptionsBannerComponent(int width, string text, Action<int> action)
    {
        AnimationManager.Add(true, new BlueButtonRegularSprite());
        DotSprite = new SquareBannerSprite();
        LineAnimation = new SmallBarBaseSprite();

        Bounds = new(0, 0, width, 96);

        SetText(text);

        ValueUpdate = action;
    }
}
