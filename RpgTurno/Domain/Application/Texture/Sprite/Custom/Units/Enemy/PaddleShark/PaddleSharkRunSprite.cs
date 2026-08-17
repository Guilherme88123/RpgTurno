using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PaddleShark;

public class PaddleSharkRunSprite : AnimationClip
{
    public PaddleSharkRunSprite() : base(
        SpriteConst.PaddleSharkRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
