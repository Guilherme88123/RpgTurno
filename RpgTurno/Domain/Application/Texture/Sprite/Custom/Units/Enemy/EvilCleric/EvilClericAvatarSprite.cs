using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilCleric;

public class EvilClericAvatarSprite : SpriteData
{
    public EvilClericAvatarSprite() : base(
        SpriteConst.EvilClericAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
