using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Lancer;

public class LancerAttackSprite : AnimationClip
{
    public LancerAttackSprite() : base(
        SpriteConst.LancerAttack,
        framesX: 3,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
