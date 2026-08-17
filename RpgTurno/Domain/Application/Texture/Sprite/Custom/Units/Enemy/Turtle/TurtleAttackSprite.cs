using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;

public class TurtleAttackSprite : AnimationClip
{
    public TurtleAttackSprite() : base(
        SpriteConst.TurtleAttack,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}