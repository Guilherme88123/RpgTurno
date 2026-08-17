using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanAttackSprite : AnimationClip
{
    public HexShamanAttackSprite() : base(
        SpriteConst.HexShamanAttack,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
