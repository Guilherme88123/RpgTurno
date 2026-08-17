using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Bear;

public class BearAvatarSprite : SpriteData
{
    public BearAvatarSprite() : base(
        SpriteConst.BearAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
