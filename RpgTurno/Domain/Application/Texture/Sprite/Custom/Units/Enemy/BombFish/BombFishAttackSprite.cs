using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.BombFish;

public class BombFishAttackSprite : AnimationClip
{
    public BombFishAttackSprite() : base(
        SpriteConst.BombFishAttack,
        framesX: 7,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
