using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollRunSprite : AnimationClip
{
    public TrollRunSprite() : base(
        SpriteConst.TrollRun,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
