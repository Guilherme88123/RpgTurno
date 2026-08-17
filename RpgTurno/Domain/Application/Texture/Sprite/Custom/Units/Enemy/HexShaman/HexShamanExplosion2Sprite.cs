using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanExplosion2Sprite : AnimationClip
{
    public HexShamanExplosion2Sprite() : base(
        SpriteConst.HexShamanExplosion2,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
