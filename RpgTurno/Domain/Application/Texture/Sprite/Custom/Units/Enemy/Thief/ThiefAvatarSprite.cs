using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Thief;

public class ThiefAvatarSprite : SpriteData
{
    public ThiefAvatarSprite() : base(
        SpriteConst.ThiefAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
