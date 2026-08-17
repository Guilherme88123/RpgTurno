using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Panda;

public class PandaAttackSprite : AnimationClip
{
    public PandaAttackSprite() : base(
        SpriteConst.PandaAttack,
        framesX: 13,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
