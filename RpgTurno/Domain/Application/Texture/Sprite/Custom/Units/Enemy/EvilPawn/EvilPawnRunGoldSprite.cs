using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnRunGoldSprite : AnimationClip
{
    public EvilPawnRunGoldSprite() : base(SpriteConst.EvilPawnRunGold, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
