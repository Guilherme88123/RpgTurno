using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.PaddleShark;

public class PaddleSharkRowSprite : AnimationClip
{
    public PaddleSharkRowSprite() : base(
        SpriteConst.PaddleSharkRow,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
