using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PaddleShark;

public class PaddleSharkAvatarSprite : SpriteData
{
    public PaddleSharkAvatarSprite() : base(
        SpriteConst.PaddleSharkAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
