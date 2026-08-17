using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;

public class SpearGoblinAvatarSprite : SpriteData
{
    public SpearGoblinAvatarSprite() : base(
        SpriteConst.SpearGoblinAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}

