using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;

public class EvilWarriorIdleSprite : AnimationClip
{
    public EvilWarriorIdleSprite() : base(
        SpriteConst.EvilWarriorIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
