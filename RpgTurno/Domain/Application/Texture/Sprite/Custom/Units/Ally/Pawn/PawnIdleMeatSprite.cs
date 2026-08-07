using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnIdleMeatSprite : AnimationClip
{
    public PawnIdleMeatSprite() : base(SpriteConst.PawnIdleMeat, framesX: 8, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
