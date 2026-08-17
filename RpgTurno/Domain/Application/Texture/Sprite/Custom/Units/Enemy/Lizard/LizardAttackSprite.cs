using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Lizard;

public class LizardAttackSprite : AnimationClip
{
    public LizardAttackSprite() : base(
        SpriteConst.LizardAttack,
        framesX: 9,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
