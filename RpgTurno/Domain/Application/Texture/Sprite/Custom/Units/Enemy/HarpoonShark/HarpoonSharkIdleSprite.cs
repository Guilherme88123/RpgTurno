using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HarpoonShark;

public class HarpoonSharkIdleSprite : AnimationClip
{
    public HarpoonSharkIdleSprite() : base(
        SpriteConst.HarpoonSharkIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
