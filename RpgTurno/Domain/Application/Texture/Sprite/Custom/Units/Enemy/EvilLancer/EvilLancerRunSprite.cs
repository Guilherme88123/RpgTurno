using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilLancer;

public class EvilLancerRunSprite : AnimationClip
{
    public EvilLancerRunSprite() : base(
        SpriteConst.EvilLancerRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
