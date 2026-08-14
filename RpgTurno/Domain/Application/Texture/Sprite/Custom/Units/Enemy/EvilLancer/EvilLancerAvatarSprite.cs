using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilLancer;

public class EvilLancerAvatarSprite : SpriteData
{
    public EvilLancerAvatarSprite() 
        : base(SpriteConst.EvilLancerAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
