using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Bear;

public class BearRunSprite : AnimationClip
{
    public BearRunSprite() : base(
        SpriteConst.BearRun,
        framesX: 5,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
