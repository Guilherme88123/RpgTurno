using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Panda;

public class PandaIdleSprite : AnimationClip
{
    public PandaIdleSprite() : base(
        SpriteConst.PandaIdle,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
