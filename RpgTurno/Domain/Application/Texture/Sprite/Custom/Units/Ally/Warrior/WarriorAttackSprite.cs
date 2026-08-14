using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;

public class WarriorAttackSprite : AnimationClip
{
    public WarriorAttackSprite() : base(
        SpriteConst.WarriorAttack,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
