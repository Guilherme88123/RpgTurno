using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;

public class SupremeWarriorIdleSprite : AnimationClip
{
    public SupremeWarriorIdleSprite() : base(
        SpriteConst.SupremeWarriorIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
