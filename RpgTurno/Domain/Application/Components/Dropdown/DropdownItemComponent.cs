using Application.Model.MenuElements.Button;
using Domain.Dto.Components.Dropdown;
using Domain.Enum.Component.Button;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Components.Dropdown;

public class DropdownItemComponent : ButtonComponent
{
    public int Id => _itemDto.Id;

    private readonly DropdownComponent _parentDropdown;
    private readonly DropdownItemDto _itemDto;

    public DropdownItemComponent(DropdownComponent parentDropdown, DropdownItemDto itemDto)
    {
        _parentDropdown = parentDropdown;
        _itemDto = itemDto;

        SetText(_itemDto.Text);

        Click += OnSelectOption;

        HoverAnimation.HoverTextColor = new Color(71, 171, 169);
    }

    private void OnSelectOption()
    {
        _parentDropdown.SelectItem(_itemDto.Id);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (_itemDto.Icon is not null)
            DrawIcon(spriteBatch);
    }

    private void DrawIcon(SpriteBatch spriteBatch)
    {
        var iconWidth = 32;
        var iconHeight = 24;
        var margin = 8;

        var iconRectangle = new Rectangle(Text.Bounds.Right + margin, Bounds.Center.Y - iconHeight / 2, iconWidth, iconHeight);
        _itemDto.Icon.Draw(iconRectangle, Color, Rotation, SpriteEffects, spriteBatch, Scale, Offset);
    }
}
