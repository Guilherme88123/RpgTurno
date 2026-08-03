using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Maps;

public class WorldMapBackgroundSprite : SpriteData
{
    public WorldMapBackgroundSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WorldMapBackground))
    {
    }
}
