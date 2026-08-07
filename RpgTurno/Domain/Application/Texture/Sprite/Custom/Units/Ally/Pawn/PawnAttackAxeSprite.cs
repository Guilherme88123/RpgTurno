using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnAttackAxeSprite : AnimationClip
{
    public PawnAttackAxeSprite() : base(SpriteConst.PawnAttackAxe, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
