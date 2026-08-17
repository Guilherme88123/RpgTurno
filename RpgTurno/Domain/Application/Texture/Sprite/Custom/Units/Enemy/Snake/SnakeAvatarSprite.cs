using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Snake;

public class SnakeAvatarSprite : SpriteData
{
    public SnakeAvatarSprite() : base(
        SpriteConst.SnakeAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}
