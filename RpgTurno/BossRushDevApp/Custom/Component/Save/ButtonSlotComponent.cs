using Application.Model.MenuElements.Button;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Enum.Component.Button;

namespace RpgTurno.Custom.Component.Save;

public class ButtonSlotComponent : ButtonComponent
{
    public ButtonSlotComponent(SpriteData sprite)
    {
        AnimationManager.Add(ButtonInteractionState.Regular, sprite);
        AnimationManager.Add(ButtonInteractionState.Pressed, sprite);

        HoverAnimation.HoverScaleX = 1.1f;
        HoverAnimation.HoverScaleY = 1.1f;
        HoverAnimation.HoverOffsetY = -15;
    }
}
