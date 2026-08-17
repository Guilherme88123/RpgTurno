using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;

public class SpearGoblinAttackStrongSprite : AnimationClip
{
    public SpearGoblinAttackStrongSprite() : base(
        SpriteConst.SpearGoblinAttackStrong,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}

