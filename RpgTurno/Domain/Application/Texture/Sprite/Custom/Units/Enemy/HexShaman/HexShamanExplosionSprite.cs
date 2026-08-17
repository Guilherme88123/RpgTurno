using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanExplosionSprite : AnimationClip
{
    public HexShamanExplosionSprite() : base(
        SpriteConst.HexShamanExplosion,
        framesX: 9,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
