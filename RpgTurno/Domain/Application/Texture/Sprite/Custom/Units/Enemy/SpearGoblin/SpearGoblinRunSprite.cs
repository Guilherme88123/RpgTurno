using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;

public class SpearGoblinRunSprite : AnimationClip
{
    public SpearGoblinRunSprite() : base(
        SpriteConst.SpearGoblinRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}

