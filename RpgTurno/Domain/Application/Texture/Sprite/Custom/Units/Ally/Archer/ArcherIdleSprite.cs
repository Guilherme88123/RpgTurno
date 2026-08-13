using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Archer;

public class ArcherIdleSprite : AnimationClip
{
    public ArcherIdleSprite() : base(
        SpriteConst.ArcherIdle, 
        framesX: 6, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1, 
        border: null)
    {
    }
}
