using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnRunMeatSprite : AnimationClip
{
    public EvilPawnRunMeatSprite() : base(SpriteConst.EvilPawnRunMeat, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
