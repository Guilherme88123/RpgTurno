using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnRunAxeSprite : AnimationClip
{
    public PawnRunAxeSprite() : base(SpriteConst.PawnRunAxe, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
