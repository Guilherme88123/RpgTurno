using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;

public class WarriorIdleSprite : AnimationClip
{
    public WarriorIdleSprite() : base(
        SpriteConst.WarriorIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
