using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollRecoverySprite : AnimationClip
{
    public TrollRecoverySprite() : base(
        SpriteConst.TrollRecovery,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
