using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Spider;

public class SpiderIdleSprite : AnimationClip
{
    public SpiderIdleSprite() : base(
        SpriteConst.SpiderIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
