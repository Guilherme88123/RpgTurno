using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilArcher;

public class EvilArcherAvatarSprite : SpriteData
{
    public EvilArcherAvatarSprite() : base(
        SpriteConst.EvilArcherAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
