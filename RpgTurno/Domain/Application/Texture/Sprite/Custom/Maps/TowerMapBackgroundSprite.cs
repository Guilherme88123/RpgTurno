using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Maps;

public class TowerMapBackgroundSprite : SpriteData
{
    public TowerMapBackgroundSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.TowerMapBackground))
    {
    }
}
