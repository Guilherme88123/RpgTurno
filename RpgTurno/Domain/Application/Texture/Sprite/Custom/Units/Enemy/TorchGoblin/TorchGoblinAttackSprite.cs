using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.TorchGoblin;

public class TorchGoblinAttackSprite : AnimationClip
{
    public TorchGoblinAttackSprite() : base(
        SpriteConst.TorchGoblinAttack,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
