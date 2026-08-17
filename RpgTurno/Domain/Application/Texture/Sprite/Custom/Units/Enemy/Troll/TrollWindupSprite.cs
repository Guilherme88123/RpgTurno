using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollWindupSprite : AnimationClip
{
    public TrollWindupSprite() : base(
        SpriteConst.TrollRecovery,
        framesX: 5,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
