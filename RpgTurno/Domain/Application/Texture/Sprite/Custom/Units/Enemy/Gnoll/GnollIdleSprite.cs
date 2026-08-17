using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;

public class GnollIdleSprite : AnimationClip
{
    public GnollIdleSprite() : base(
        SpriteConst.GnollIdle,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
