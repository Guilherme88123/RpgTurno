using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;

public class EvilWarriorRunSprite : AnimationClip
{
    public EvilWarriorRunSprite() : base(
        SpriteConst.EvilWarriorRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
