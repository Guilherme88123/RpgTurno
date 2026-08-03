using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Application.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Banners;

public class SquareBannerSprite : SpriteData
{
    public SquareBannerSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SquareBanner), border: new(0, 8, 7, 7))
    {
    }
}
