using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Pig;

public class PigRunSprite : AnimationClip
{
    public PigRunSprite() : base(
        SpriteConst.PigRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
