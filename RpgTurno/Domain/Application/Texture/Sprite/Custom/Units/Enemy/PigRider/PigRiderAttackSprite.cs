using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PigRider;

public class PigRiderAttackSprite : AnimationClip
{
    public PigRiderAttackSprite() : base(
        SpriteConst.PigRiderAttack,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
