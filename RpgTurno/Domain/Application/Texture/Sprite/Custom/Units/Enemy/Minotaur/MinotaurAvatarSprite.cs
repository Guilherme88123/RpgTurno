using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Minotaur;

public class MinotaurAvatarSprite : SpriteData
{
    public MinotaurAvatarSprite() : base(
        SpriteConst.MinotaurAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
