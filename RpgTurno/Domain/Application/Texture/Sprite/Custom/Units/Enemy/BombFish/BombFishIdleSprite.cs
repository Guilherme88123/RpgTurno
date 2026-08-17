using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.BombFish;

public class BombFishIdleSprite : AnimationClip
{
    public BombFishIdleSprite() : base(
        SpriteConst.BombFishIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
