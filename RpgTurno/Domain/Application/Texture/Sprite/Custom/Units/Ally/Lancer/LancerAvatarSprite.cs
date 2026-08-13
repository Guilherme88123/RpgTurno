using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Lancer;

public class LancerAvatarSprite : SpriteData
{
    public LancerAvatarSprite() 
        : base(SpriteConst.LancerAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
