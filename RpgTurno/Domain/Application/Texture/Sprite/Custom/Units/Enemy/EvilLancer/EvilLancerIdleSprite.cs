using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilLancer;

public class EvilLancerIdleSprite : AnimationClip
{
    public EvilLancerIdleSprite() : base(
        SpriteConst.EvilLancerIdle,
        framesX: 12,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
