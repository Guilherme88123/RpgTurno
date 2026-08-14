using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;

public class EvilWarriorGuardSprite : AnimationClip
{
    public EvilWarriorGuardSprite() : base(
        SpriteConst.EvilWarriorGuard,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
