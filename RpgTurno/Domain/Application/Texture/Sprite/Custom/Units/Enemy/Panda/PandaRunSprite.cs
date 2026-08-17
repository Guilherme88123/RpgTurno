using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Panda;

public class PandaRunSprite : AnimationClip
{
    public PandaRunSprite() : base(
        SpriteConst.PandaRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
