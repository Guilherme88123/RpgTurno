using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Spider;

public class SpiderAttackSprite : AnimationClip
{
    public SpiderAttackSprite() : base(
        SpriteConst.SpiderAttack,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
