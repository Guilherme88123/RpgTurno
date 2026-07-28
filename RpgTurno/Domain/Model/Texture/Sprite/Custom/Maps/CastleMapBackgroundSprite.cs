using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Maps;

public class CastleMapBackgroundSprite : SpriteData
{
    public CastleMapBackgroundSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.CastleMapBackground))
    {
    }
}
