using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnAttackAxeSprite : AnimationClip
{
    public EvilPawnAttackAxeSprite() : base(SpriteConst.EvilPawnAttackAxe, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
