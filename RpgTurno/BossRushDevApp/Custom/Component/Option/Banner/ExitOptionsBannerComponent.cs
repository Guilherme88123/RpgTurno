using Application.Model.MenuElements.Button;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Dto.Language;
using Domain.Enum.Component.Button;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;

namespace RpgTurno.Custom.Component.Option.Banner;

public class ExitOptionsBannerComponent : ButtonComponent
{
    public ExitOptionsBannerComponent()
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new RedButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new RedButtonPressedSprite());

        Bounds = new(0, 0, 224, 96);

        Click = () => GlobalVariablesDto.PopScreen();

        Text.SetText(LanguageManager.Get(TextConst.Exit));
    }
}
