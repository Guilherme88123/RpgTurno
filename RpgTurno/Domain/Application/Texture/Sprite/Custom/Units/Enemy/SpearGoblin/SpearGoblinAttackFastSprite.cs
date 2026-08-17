using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;

public class SpearGoblinAttackFastSprite : AnimationClip
{
    public SpearGoblinAttackFastSprite() : base(
        SpriteConst.SpearGoblinAttackFast,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}

