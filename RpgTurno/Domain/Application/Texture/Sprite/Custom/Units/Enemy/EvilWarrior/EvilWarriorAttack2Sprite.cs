using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;

public class EvilWarriorAttack2Sprite : AnimationClip
{
    public EvilWarriorAttack2Sprite() : base(
        SpriteConst.EvilWarriorAttack2,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
