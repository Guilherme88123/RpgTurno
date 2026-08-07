using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnIdleSprite : AnimationClip
{
    public EvilPawnIdleSprite() : base(SpriteConst.EvilPawnIdle, framesX: 8, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
