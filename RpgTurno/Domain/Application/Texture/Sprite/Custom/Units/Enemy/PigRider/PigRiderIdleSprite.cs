using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PigRider;

public class PigRiderIdleSprite : AnimationClip
{
    public PigRiderIdleSprite() : base(
        SpriteConst.PigRiderIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
