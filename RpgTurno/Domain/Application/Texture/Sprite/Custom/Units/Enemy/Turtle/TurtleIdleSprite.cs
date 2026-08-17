using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;

public class TurtleIdleSprite : AnimationClip
{
    public TurtleIdleSprite() : base(
        SpriteConst.TurtleIdle,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}