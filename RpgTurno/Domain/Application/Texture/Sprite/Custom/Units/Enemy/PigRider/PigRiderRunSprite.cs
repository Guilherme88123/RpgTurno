using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PigRider;

public class PigRiderRunSprite : AnimationClip
{
    public PigRiderRunSprite() : base(
        SpriteConst.PigRiderRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
