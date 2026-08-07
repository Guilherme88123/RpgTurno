using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnAvatarSprite : SpriteData
{
    public PawnAvatarSprite() : base(SpriteConst.PawnAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
