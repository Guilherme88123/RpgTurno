using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class ClericAvatarSprite : SpriteData
{
    public ClericAvatarSprite()
        : base(SpriteConst.ClericAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
