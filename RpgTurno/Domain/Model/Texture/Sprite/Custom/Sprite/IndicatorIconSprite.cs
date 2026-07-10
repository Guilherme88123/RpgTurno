using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite;

public class IndicatorIconSprite : SpriteData
{
    public IndicatorIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.IndicatorIcon))
    {
    }
}
