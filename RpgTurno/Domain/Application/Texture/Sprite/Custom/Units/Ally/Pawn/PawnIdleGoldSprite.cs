using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnIdleGoldSprite : AnimationClip
{
    public PawnIdleGoldSprite() : base(SpriteConst.PawnIdleGold, framesX: 8, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
