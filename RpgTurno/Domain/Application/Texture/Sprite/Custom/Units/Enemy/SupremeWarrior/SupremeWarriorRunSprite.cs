using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;

public class SupremeWarriorRunSprite : AnimationClip
{
    public SupremeWarriorRunSprite() : base(
        SpriteConst.SupremeWarriorRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
