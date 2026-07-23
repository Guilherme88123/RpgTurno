using Application.Model.MenuElements.Switch;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;
using System;

namespace RpgTurno.Custom.Component.Option.Banner;

public class SwitchOptionsBannerComponent : SwitchComponent
{
    public SwitchOptionsBannerComponent(int width, string text, Action<bool> action)
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        Bounds = new(0, 0, width, 96);

        SetText(text);

        ValueUpdate = action;
    }
}
