using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Skull;

public class SkullAttackSprite : AnimationClip
{
    public SkullAttackSprite() : base(
        SpriteConst.SkullAttack,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
