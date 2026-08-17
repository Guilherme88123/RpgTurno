using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Thief;

public class ThiefAttackSprite : AnimationClip
{
    public ThiefAttackSprite() : base(
        SpriteConst.ThiefAttack,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
