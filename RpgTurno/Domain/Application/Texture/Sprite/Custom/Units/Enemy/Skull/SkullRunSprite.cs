using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Skull;

public class SkullRunSprite : AnimationClip
{
    public SkullRunSprite() : base(
        SpriteConst.SkullRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
