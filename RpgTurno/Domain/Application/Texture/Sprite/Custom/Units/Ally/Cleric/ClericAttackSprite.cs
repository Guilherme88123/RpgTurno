using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Cleric;

public class ClericAttackSprite : AnimationClip
{
    public ClericAttackSprite() : base(
        SpriteConst.ClericAttack,
        framesX: 11,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
