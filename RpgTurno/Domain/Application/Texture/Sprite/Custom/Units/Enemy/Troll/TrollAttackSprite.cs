using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollAttackSprite : AnimationClip
{
    public TrollAttackSprite() : base(
        SpriteConst.TrollAttack,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
