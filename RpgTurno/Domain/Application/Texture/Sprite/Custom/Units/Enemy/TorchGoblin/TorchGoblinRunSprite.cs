using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.TorchGoblin;

public class TorchGoblinRunSprite : AnimationClip
{
    public TorchGoblinRunSprite() : base(
        SpriteConst.TorchGoblinRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
