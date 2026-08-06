using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;

public class WarriorGuardSprite : AnimationClip
{
    public WarriorGuardSprite() : base(SpriteConst.WarriorGuard, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
