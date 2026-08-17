using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Minotaur;

public class MinotaurAttackSprite : AnimationClip
{
    public MinotaurAttackSprite() : base(
        SpriteConst.MinotaurAttack,
        framesX: 12,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
