using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Minotaur;

public class MinotaurIdleSprite : AnimationClip
{
    public MinotaurIdleSprite() : base(
        SpriteConst.MinotaurIdle,
        framesX: 16,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
