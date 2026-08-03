using Application.Model.MenuElements.Button;
using Domain.Enum.Component.Button;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using System;

namespace RpgTurno.Custom.Component.Map.Banner.Finish;

public class ButtonGameFinishBannerComponent : ButtonComponent
{
    public ButtonGameFinishBannerComponent(string text, Action action)
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        Text.SetText(text);

        Click = action;

        Bounds = new(0, 0, 272, 96);
    }
}
