using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnome;

public class GnomeAttackSprite : AnimationClip
{
    public GnomeAttackSprite() : base(
        SpriteConst.GnomeAttack,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}

