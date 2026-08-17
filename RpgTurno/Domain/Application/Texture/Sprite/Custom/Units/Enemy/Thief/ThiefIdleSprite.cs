using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Thief;

public class ThiefIdleSprite : AnimationClip
{
    public ThiefIdleSprite() : base(
        SpriteConst.ThiefIdle,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
