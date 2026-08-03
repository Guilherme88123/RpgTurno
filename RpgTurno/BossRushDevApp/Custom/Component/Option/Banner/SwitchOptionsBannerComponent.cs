using Application.Model.MenuElements.Switch;
using Domain.Enum.Component.Button;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using System;

namespace RpgTurno.Custom.Component.Option.Banner;

public class SwitchOptionsBannerComponent : SwitchComponent
{
    public SwitchOptionsBannerComponent(int width, int height, string text, Action<bool> action)
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        Bounds = new(0, 0, width, height);

        SetText(text);

        ValueUpdate = action;
    }
}
