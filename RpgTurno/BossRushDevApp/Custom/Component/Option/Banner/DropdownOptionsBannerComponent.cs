using Domain.Dto.Components.Dropdown;
using Domain.Enum.Component.Button;
using Domain.Application.Components.Dropdown;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using System;
using System.Collections.Generic;

namespace RpgTurno.Custom.Component.Option.Banner;

public class DropdownOptionsBannerComponent : DropdownComponent
{
    public DropdownOptionsBannerComponent(int width, int height, string text, Action<DropdownItemDto> action, List<DropdownItemDto> itens)
        : base(itens)
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        OptionsOverlaySprite = new PaperBannerSprite();
        SelectedIndicatorSprite = new ConfirmIconSprite();

        Bounds = new(0, 0, width, height);

        ValueUpdate = action;

        SetText(text);
    }
}
