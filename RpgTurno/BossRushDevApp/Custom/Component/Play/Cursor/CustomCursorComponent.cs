using Domain.Model.Components.Cursor;
using Domain.Model.Texture.Sprite.Custom.Sprite;

namespace RpgTurno.Custom.CustomComponents.Play.Cursor;

public class CustomCursorComponent : CursorComponent
{
    public CustomCursorComponent() : base(
        new NormalCursorSprite(), 
        new HoverCursorSprite(), 
        new BlockCursorSprite())
    {
    }
}
