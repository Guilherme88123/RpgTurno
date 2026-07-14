using Application.Model.MenuElements.Button;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Buttons;

namespace RpgTurno.Custom.Component.Menu.Button;

public class MenuButtonComponent : ButtonComponent
{
    private const int Spacing = 0;

    public MenuButtonComponent(bool isDanger = false)
    {
        SpriteData regularSprite = isDanger ? new RedButtonRegularSprite() : new BlueButtonRegularSprite();
        SpriteData pressedSprite = isDanger ? new RedButtonPressedSprite() : new BlueButtonPressedSprite();

        AnimationManager.Add(ButtonInteractionState.Regular, regularSprite);
        AnimationManager.Add(ButtonInteractionState.Pressed, pressedSprite);

        Bounds = new(0, 0, 320, 128);
    }

    public void SetPositionWithIndex(int initialPositionY, int index)
    {
        var positionX = GlobalOptionsDto.WidthSize / 2 - Bounds.Width / 2;
        var positionY = initialPositionY + (Bounds.Height + Spacing) * index;

        SetPosition(positionX, positionY);
    }
}
