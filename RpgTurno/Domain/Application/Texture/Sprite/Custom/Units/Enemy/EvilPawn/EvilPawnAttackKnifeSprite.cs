using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnAttackKnifeSprite : AnimationClip
{
    public EvilPawnAttackKnifeSprite() : base(SpriteConst.EvilPawnAttackKnife, framesX: 4, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
