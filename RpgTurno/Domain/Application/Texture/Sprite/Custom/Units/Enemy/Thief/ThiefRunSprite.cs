using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Thief;

public class ThiefRunSprite : AnimationClip
{
    public ThiefRunSprite() : base(
        SpriteConst.ThiefRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
