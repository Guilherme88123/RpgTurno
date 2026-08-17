using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.BombFish;

public class BombFishRunSprite : AnimationClip
{
    public BombFishRunSprite() : base(
        SpriteConst.BombFishRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
