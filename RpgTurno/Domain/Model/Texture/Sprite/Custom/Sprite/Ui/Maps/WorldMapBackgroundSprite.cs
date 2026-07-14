using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Maps;

public class WorldMapBackgroundSprite : SpriteData
{
    public WorldMapBackgroundSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WorldMapBackground))
    {
    }
}
