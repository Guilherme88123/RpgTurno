using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Snake;

public class SnakeAttackSprite : AnimationClip
{
    public SnakeAttackSprite() : base(
        SpriteConst.SnakeAttack,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
