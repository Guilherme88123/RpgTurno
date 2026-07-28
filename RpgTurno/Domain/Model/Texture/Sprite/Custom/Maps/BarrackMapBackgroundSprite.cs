using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Maps;

public class BarrackMapBackgroundSprite : SpriteData
{
    public BarrackMapBackgroundSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BarrackMapBackground))
    {
    }
}
