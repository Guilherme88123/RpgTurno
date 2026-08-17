using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanAttack2Sprite : AnimationClip
{
    public HexShamanAttack2Sprite() : base(
        SpriteConst.HexShamanAttack2,
        framesX: 11,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
