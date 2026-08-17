using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;

public class SpearGoblinIdleSprite : AnimationClip
{
    public SpearGoblinIdleSprite() : base(
        SpriteConst.SpearGoblinIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}