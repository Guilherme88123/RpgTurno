using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;

public class PawnIdleKnifeSprite : AnimationClip
{
    public PawnIdleKnifeSprite() : base(SpriteConst.PawnIdleKnife, framesX: 8, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
