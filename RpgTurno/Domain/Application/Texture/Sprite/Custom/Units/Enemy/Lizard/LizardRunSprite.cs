using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Lizard;

public class LizardRunSprite : AnimationClip
{
    public LizardRunSprite() : base(
        SpriteConst.LizardRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
