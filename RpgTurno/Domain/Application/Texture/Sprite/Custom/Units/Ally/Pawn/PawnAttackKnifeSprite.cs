using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnAttackKnifeSprite : AnimationClip
{
    public PawnAttackKnifeSprite() : base(SpriteConst.PawnAttackKnife, framesX: 4, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
