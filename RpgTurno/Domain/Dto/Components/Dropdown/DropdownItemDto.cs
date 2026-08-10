using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework;

namespace Domain.Dto.Components.Dropdown;

public class DropdownItemDto
{
    public int Id { get; set; }

    public string Text { get; set; }
    public object Value { get; set; }

    public Rectangle Rectangle { get; set; }
    public bool IsHover { get; set; }

    public SpriteData Icon { get; set; }
}
