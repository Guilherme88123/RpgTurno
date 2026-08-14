using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilArcher;

public class EvilArcherIdleSprite : AnimationClip
{
    public EvilArcherIdleSprite() : base(
        SpriteConst.EvilArcherIdle,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
