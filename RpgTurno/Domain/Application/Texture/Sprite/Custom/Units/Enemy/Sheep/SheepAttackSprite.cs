using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Sheep;

public class SheepAttackSprite : AnimationClip
{
    public SheepAttackSprite() : base(SpriteConst.SheepAttack, framesX: 12, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
