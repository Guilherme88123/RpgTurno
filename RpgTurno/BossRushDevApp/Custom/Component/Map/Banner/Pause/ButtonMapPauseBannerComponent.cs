using Application.Model.MenuElements.Button;
using Domain.Enum.Component.Button;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using System;

namespace RpgTurno.Custom.Component.Map.Banner.Pause;

public class ButtonMapPauseBannerComponent : ButtonComponent
{
    public ButtonMapPauseBannerComponent(string text, Action action, bool isDanger = false)
    {
        SpriteData regularSprite = isDanger ? new RedButtonRegularSprite() : new BlueButtonRegularSprite();
        SpriteData pressedSprite = isDanger ? new RedButtonPressedSprite() : new BlueButtonPressedSprite();

        AnimationManager.Add(ButtonInteractionState.Regular, regularSprite);
        AnimationManager.Add(ButtonInteractionState.Pressed, pressedSprite);

        Text.SetText(text);

        Click = action;

        Bounds = new(0, 0, 272, 96);
    }
}
