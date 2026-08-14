using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilCleric;

public class EvilClericIdleSprite : AnimationClip
{
    public EvilClericIdleSprite() : base(
        SpriteConst.EvilClericIdle,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
