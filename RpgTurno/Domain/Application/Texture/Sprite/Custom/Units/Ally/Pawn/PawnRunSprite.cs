using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnRunSprite : AnimationClip
{
    public PawnRunSprite() : base(SpriteConst.PawnRun, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
