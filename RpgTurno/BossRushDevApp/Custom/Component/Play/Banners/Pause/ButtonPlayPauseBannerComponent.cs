using Application.Model.MenuElements.Button;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Buttons;
using System;

namespace RpgTurno.Custom.Component.Play.Banners.Pause;

public class ButtonPlayPauseBannerComponent : ButtonComponent
{
    public ButtonPlayPauseBannerComponent(string text, Action action, bool isDanger = false)
    {
        SpriteData regularSprite = isDanger ? new RedButtonRegularSprite() : new BlueButtonRegularSprite();
        SpriteData pressedSprite = isDanger ? new RedButtonPressedSprite() : new BlueButtonPressedSprite();

        AnimationManager.Add(ButtonInteractionState.Regular, regularSprite);
        AnimationManager.Add(ButtonInteractionState.Pressed, pressedSprite);

        Text.SetText(text);

        Click = action;

        Bounds = new(0, 0, 256, 128);
    }
}
