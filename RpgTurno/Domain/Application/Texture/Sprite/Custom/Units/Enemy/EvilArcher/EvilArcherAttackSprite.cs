using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilArcher;

public class EvilArcherAttackSprite : AnimationClip
{
    public EvilArcherAttackSprite() : base(
        SpriteConst.EvilArcherAttack,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
