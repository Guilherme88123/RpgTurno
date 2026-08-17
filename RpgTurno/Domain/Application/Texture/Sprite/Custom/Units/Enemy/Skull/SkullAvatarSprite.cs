using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Skull;

public class SkullAvatarSprite : SpriteData
{
    public SkullAvatarSprite() : base(
        SpriteConst.SkullAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
