using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnome;

public class GnomeRunSprite : AnimationClip
{
    public GnomeRunSprite() : base(
        SpriteConst.GnomeRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}

