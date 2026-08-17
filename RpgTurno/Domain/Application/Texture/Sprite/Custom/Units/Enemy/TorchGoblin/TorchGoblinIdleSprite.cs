using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.TorchGoblin;

public class TorchGoblinIdleSprite : AnimationClip
{
    public TorchGoblinIdleSprite() : base(
        SpriteConst.TorchGoblinIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
