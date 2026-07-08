using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite;

public class ScrollMiddleBannerSprite : SpriteData
{
    public ScrollMiddleBannerSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ScrollBanner), border: new BorderDefinition(208, 208, 208, 208))
    {
    }
}
