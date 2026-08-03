using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Cursor;

public class BlockCursorSprite : SpriteData
{
    public BlockCursorSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlockCursor))
    {
    }
}
