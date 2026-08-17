using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Snake;

public class SnakeRunSprite : AnimationClip
{
    public SnakeRunSprite() : base(
        SpriteConst.SnakeRun,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
