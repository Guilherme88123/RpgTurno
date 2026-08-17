using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;

public class HexShamanRunSprite : AnimationClip
{
    public HexShamanRunSprite() : base(
        SpriteConst.HexShamanRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
