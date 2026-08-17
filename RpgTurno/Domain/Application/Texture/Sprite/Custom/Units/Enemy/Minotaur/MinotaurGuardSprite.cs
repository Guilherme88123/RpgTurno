using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Minotaur;

public class MinotaurGuardSprite : AnimationClip
{
    public MinotaurGuardSprite() : base(
        SpriteConst.MinotaurGuard,
        framesX: 11,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
