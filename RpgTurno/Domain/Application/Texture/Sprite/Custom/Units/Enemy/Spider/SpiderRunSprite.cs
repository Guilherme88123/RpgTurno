using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Spider;

public class SpiderRunSprite : AnimationClip
{
    public SpiderRunSprite() : base(
        SpriteConst.SpiderRun,
        framesX: 5,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
