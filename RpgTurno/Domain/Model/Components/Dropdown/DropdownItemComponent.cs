using Application.Model.MenuElements.Button;
using Application.Model.MenuElements.Dropdown;
using Domain.Dto.Components.Dropdown;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite.Custom.Ui.Buttons;
using Microsoft.Xna.Framework;

namespace Domain.Model.Components.Dropdown;

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

    public void SetBounds(int width, int height)
    {
        Bounds = new(Bounds.X, Bounds.Y, width, height);
    }

    private void OnSelectOption()
    {
        _parentDropdown.SelectItem(_itemDto.Id);
    }
}
