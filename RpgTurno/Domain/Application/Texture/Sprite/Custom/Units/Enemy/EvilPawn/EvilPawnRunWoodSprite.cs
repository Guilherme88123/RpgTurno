using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;

public class EvilPawnRunWoodSprite : AnimationClip
{
    public EvilPawnRunWoodSprite() : base(SpriteConst.EvilPawnRunWood, framesX: 6, framesY: 1, frameTime: 0.1f, row: 1, border: null)
    {
    }
}
