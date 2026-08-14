using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;

public class WarriorAttack2Sprite : AnimationClip
{
    public WarriorAttack2Sprite() : base(
        SpriteConst.WarriorAttack2,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
