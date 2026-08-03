using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Cursor;

public class NormalCursorSprite : SpriteData
{
    public NormalCursorSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.NormalCursor))
    {
    }
}
