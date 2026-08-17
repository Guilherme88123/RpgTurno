using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Panda;

public class PandaGuardSprite : AnimationClip
{
    public PandaGuardSprite() : base(
        SpriteConst.PandaGuard,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
