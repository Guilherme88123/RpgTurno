using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnome;

public class GnomeIdleSprite : AnimationClip
{
    public GnomeIdleSprite() : base(
        SpriteConst.GnomeIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}

