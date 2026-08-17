using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Sheep;

public class SheepIdleSprite : AnimationClip
{
    public SheepIdleSprite() : base(SpriteConst.SheepIdle, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
