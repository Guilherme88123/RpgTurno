using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnRunMeatSprite : AnimationClip
{
    public PawnRunMeatSprite() : base(SpriteConst.PawnRunMeat, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
