using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Ally.Cleric;

public class ClericRunSprite : AnimationClip
{
    public ClericRunSprite() : base(
        SpriteConst.ClericRun,
        framesX: 4,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
