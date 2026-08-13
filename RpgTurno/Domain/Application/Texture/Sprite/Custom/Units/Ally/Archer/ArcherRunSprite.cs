using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Archer;

public class ArcherRunSprite : AnimationClip
{
    public ArcherRunSprite() : base(
        SpriteConst.ArcherRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
