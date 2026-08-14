using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilArcher;

public class EvilArcherRunSprite : AnimationClip
{
    public EvilArcherRunSprite() : base(
        SpriteConst.EvilArcherRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
