using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;

public class GnollAvatarSprite : SpriteData
{
    public GnollAvatarSprite() : base(
        SpriteConst.GnollAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
