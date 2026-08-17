using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PaddleShark;

public class PaddleSharkAttackSprite : AnimationClip
{
    public PaddleSharkAttackSprite() : base(
        SpriteConst.PaddleSharkAttack,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
