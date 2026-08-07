using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Sheep;

public class SheepRunSprite : AnimationClip
{
    public SheepRunSprite() : base(SpriteConst.SheepRun, framesX: 4, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
