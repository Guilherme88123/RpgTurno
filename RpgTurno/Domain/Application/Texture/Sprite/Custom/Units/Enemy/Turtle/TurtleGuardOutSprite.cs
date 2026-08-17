using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;

public class TurtleGuardOutSprite : AnimationClip
{
    public TurtleGuardOutSprite() : base(
        SpriteConst.TurtleGuardOut,
        framesX: 3,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}