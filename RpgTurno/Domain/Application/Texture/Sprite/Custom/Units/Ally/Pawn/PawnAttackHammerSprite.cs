using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnAttackHammerSprite : AnimationClip
{
    public PawnAttackHammerSprite() : base(SpriteConst.PawnAttackHammer, framesX: 3, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
