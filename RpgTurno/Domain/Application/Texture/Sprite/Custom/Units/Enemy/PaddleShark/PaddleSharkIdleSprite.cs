using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PaddleShark;

public class PaddleSharkIdleSprite : AnimationClip
{
    public PaddleSharkIdleSprite() : base(
        SpriteConst.PaddleSharkIdle,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
