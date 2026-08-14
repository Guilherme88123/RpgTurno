using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilCleric;

public class EvilClericRunSprite : AnimationClip
{
    public EvilClericRunSprite() : base(
        SpriteConst.EvilClericRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
