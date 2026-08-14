using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;

public class SupremeWarriorAttack2Sprite : AnimationClip
{
    public SupremeWarriorAttack2Sprite() : base(
        SpriteConst.SupremeWarriorAttack2,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
