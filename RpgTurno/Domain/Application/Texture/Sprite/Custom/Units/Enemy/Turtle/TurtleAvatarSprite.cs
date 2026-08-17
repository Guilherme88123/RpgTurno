using Domain.Application.Sprite.Border;
using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;

public class TurtleAvatarSprite : SpriteData
{
    public TurtleAvatarSprite() : base(
        SpriteConst.TurtleAvatar,
        border: new BorderDefinition(16, 16, 16, 16))
    {
    }
}