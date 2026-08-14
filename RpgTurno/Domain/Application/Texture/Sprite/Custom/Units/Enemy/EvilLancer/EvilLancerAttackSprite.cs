using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilLancer;

public class EvilLancerAttackSprite : AnimationClip
{
    public EvilLancerAttackSprite() : base(
        SpriteConst.EvilLancerAttack,
        framesX: 3,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
