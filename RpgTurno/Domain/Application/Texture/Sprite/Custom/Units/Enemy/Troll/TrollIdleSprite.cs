using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollIdleSprite : AnimationClip
{
    public TrollIdleSprite() : base(
        SpriteConst.TrollIdle,
        framesX: 12,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
