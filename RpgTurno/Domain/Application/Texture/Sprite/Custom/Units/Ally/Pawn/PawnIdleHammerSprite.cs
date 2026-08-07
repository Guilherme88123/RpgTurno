using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnIdleHammerSprite : AnimationClip
{
    public PawnIdleHammerSprite() : base(SpriteConst.PawnIdleHammer, framesX: 8, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
