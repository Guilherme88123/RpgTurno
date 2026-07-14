using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Cursor;

public class NormalCursorSprite : SpriteData
{
    public NormalCursorSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.NormalCursor))
    {
    }
}
