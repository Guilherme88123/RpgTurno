using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Lancer;

public class LancerRunSprite : AnimationClip
{
    public LancerRunSprite() : base(
        SpriteConst.LancerRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
