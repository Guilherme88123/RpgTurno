using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;

public class GnollHitSprite : AnimationClip
{
    public GnollHitSprite() : base(
        SpriteConst.GnollHit,
        framesX: 2,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
