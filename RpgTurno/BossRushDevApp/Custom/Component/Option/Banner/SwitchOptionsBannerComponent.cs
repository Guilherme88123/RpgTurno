using Application.Model.MenuElements.Switch;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

namespace RpgTurno.Custom.Component.Option.Banner;

public class SwitchOptionsBannerComponent : SwitchComponent
{
    public SwitchOptionsBannerComponent(int width, string text)
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        Bounds = new(0, 0, width, 128);

        SetText(text);
    }
}
