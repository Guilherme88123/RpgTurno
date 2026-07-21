using Application.Model.MenuElements.Button;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

namespace RpgTurno.Custom.Component.Option.Banner;

public class ExitOptionsBannerComponent : ButtonComponent
{
    public ExitOptionsBannerComponent()
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new SmallRedRoundButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new SmallRedRoundButtonPressedSprite());

        Bounds = new(0, 0, 128, 128);

        Click = () => GlobalVariablesDto.PopScreen();

        Text.SetText("X");
    }
}
