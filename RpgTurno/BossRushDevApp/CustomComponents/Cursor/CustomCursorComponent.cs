using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Components.Cursor;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.CustomComponents.Cursor;

public class CustomCursorComponent : CursorComponent
{
    public CustomCursorComponent() : 
        base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.NormalCursor), 
             GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HoverCursor), 
             GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlockCursor))
    {
    }
}
