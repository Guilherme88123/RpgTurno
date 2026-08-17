using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Lizard;

public class LizardHitSprite : AnimationClip
{
    public LizardHitSprite() : base(
        SpriteConst.LizardHit,
        framesX: 2,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
