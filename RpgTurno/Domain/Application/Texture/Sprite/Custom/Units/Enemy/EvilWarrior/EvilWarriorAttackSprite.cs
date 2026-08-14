using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;

public class EvilWarriorAttackSprite : AnimationClip
{
    public EvilWarriorAttackSprite() : base(
        SpriteConst.EvilWarriorAttack,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
