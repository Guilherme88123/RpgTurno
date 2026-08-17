using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollDeadSprite : AnimationClip
{
    public TrollDeadSprite() : base(
        SpriteConst.TrollDead,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
