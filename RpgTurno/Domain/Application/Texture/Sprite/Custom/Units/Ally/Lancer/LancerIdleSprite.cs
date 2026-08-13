using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Lancer;

public class LancerIdleSprite : AnimationClip
{
    public LancerIdleSprite() : base(
        SpriteConst.LancerIdle,
        framesX: 12,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
