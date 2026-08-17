using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Bear;

public class BearAttackSprite : AnimationClip
{
    public BearAttackSprite() : base(
        SpriteConst.BearAttack,
        framesX: 9,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
