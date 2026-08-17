using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;

public class TurtleRunSprite : AnimationClip
{
    public TurtleRunSprite() : base(
        SpriteConst.TurtleRun,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}