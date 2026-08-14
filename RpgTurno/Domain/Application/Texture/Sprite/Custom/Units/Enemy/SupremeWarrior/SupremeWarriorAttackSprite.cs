using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;

public class SupremeWarriorAttackSprite : AnimationClip
{
    public SupremeWarriorAttackSprite() : base(
        SpriteConst.SupremeWarriorAttack,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
