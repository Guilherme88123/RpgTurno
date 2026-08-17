using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Lizard;

public class LizardIdleSprite : AnimationClip
{
    public LizardIdleSprite() : base(
        SpriteConst.LizardIdle,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
