using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Spider;

public class SpiderAvatarSprite : SpriteData
{
    public SpiderAvatarSprite() : base(
        SpriteConst.SpiderAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
