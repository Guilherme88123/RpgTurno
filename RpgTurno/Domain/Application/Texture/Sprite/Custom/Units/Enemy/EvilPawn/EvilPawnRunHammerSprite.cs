using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnRunHammerSprite : AnimationClip
{
    public EvilPawnRunHammerSprite() : base(SpriteConst.EvilPawnRunHammer, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
