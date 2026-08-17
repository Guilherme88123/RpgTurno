using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;

public class GnollRunSprite : AnimationClip
{
    public GnollRunSprite() : base(
        SpriteConst.GnollRun,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
