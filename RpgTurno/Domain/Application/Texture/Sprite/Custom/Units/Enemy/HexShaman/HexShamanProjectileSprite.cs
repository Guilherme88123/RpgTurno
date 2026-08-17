using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanProjectileSprite : AnimationClip
{
    public HexShamanProjectileSprite() : base(
        SpriteConst.HexShamanProjectile,
        framesX: 3,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
