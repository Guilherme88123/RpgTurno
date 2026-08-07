using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnAttackHammerSprite : AnimationClip
{
    public EvilPawnAttackHammerSprite() : base(SpriteConst.EvilPawnAttackHammer, framesX: 3, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
