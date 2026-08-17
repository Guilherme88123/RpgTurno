using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;

public class GnollAttackSprite : AnimationClip
{
    public GnollAttackSprite() : base(
        SpriteConst.GnollAttack,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
