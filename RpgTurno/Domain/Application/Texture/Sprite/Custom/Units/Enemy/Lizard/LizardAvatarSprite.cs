using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Lizard;

public class LizardAvatarSprite : SpriteData
{
    public LizardAvatarSprite() : base(
        SpriteConst.LizardAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
