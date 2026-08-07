using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnAvatarSprite : SpriteData
{
    public EvilPawnAvatarSprite() : base(SpriteConst.EvilPawnAvatar, border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
