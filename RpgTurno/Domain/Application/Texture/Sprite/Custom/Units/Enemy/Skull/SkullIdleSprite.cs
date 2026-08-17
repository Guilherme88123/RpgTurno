using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Skull;

public class SkullIdleSprite : AnimationClip
{
    public SkullIdleSprite() : base(
        SpriteConst.SkullIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
