using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnome;

public class GnomeAvatarSprite : SpriteData
{
    public GnomeAvatarSprite() : base(
        SpriteConst.GnomeAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}

