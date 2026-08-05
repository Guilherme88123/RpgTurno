using Domain.Application.Components.Button;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Enum.Component.Button;
using System;

namespace RpgTurno.Custom.Component.Save;

public class DeleteButtonSlotComponent : ButtonIconComponent
{
    public DeleteButtonSlotComponent(Action onClick) : base(new CloseIconSprite())
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new SmallRedRoundButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new SmallRedRoundButtonPressedSprite());

        Bounds = new(0, 0, 124, 124);

        Click += onClick;
    }
}
