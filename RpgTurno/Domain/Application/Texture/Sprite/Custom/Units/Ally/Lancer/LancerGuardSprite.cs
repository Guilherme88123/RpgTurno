using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Lancer;

public class LancerGuardSprite : AnimationClip
{
    public LancerGuardSprite() : base(
        SpriteConst.LancerGuard,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
