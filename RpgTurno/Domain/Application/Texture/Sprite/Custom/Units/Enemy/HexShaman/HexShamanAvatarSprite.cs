using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanAvatarSprite : SpriteData
{
    public HexShamanAvatarSprite() : base(
        SpriteConst.HexShamanAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
