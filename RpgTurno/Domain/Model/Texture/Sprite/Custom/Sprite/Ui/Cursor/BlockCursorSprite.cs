using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Cursor;

public class BlockCursorSprite : SpriteData
{
    public BlockCursorSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlockCursor))
    {
    }
}
