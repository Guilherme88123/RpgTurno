using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Minotaur;

public class MinotaurRunSprite : AnimationClip
{
    public MinotaurRunSprite() : base(
        SpriteConst.MinotaurRun,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
