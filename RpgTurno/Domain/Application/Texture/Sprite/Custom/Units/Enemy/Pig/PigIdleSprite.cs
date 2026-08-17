using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Pig;

public class PigIdleSprite : AnimationClip
{
    public PigIdleSprite() : base(
        SpriteConst.PigIdle,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
