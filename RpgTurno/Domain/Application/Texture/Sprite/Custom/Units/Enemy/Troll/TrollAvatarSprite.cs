using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollAvatarSprite : SpriteData
{
    public TrollAvatarSprite() : base(
        SpriteConst.TrollAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
