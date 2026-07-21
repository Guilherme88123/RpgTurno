using Application.Model.MenuElements.Button;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

namespace RpgTurno.Custom.Component.Option.Banner;

public class ExitOptionsBannerComponent : ButtonComponent
{
    public ExitOptionsBannerComponent()
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new RedButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new RedButtonPressedSprite());

        Bounds = new(0, 0, 256, 128);

        Click = () => GlobalVariablesDto.PopScreen();

        Text.SetText("Exit");
    }
}
