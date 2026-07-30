using Application.Model.MenuElements.Dropdown;
using Domain.Dto.Components.Dropdown;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;
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

        Bounds = new(0, 0, width, height);

        ValueUpdate = action;

        SetText(text);
    }
}
