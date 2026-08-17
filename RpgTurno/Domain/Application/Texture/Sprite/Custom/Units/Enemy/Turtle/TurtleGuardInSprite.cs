using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;

public class TurtleGuardInSprite : AnimationClip
{
    public TurtleGuardInSprite() : base(
        SpriteConst.TurtleGuardIn,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}