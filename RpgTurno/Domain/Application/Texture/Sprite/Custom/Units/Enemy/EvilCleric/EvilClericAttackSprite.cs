using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilCleric;

public class EvilClericAttackSprite : AnimationClip
{
    public EvilClericAttackSprite() : base(
        SpriteConst.EvilClericAttack,
        framesX: 11,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
