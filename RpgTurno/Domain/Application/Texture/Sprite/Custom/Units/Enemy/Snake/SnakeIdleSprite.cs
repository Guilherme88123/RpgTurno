using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Snake;

public class SnakeIdleSprite : AnimationClip
{
    public SnakeIdleSprite() : base(
        SpriteConst.SnakeIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
