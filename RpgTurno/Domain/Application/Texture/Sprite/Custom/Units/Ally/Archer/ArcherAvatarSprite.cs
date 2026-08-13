using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Archer;

public class ArcherAvatarSprite : SpriteData
{
    public ArcherAvatarSprite() 
        : base(SpriteConst.ArcherAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
