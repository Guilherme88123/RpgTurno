using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilLancer;

public class EvilLancerGuardSprite : AnimationClip
{
    public EvilLancerGuardSprite() : base(
        SpriteConst.EvilLancerGuard,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
