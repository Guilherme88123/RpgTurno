using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Bear;

public class BearIdleSprite : AnimationClip
{
    public BearIdleSprite() : base(
        SpriteConst.BearIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
