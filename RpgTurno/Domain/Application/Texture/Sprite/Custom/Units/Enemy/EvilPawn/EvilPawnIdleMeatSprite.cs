using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnIdleMeatSprite : AnimationClip
{
    public EvilPawnIdleMeatSprite() : base(SpriteConst.EvilPawnIdleMeat, framesX: 8, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
