using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.HarpoonShark;

public class HarpoonSharkRunSprite : AnimationClip
{
    public HarpoonSharkRunSprite() : base(
        SpriteConst.HarpoonSharkRun,
        framesX: 6,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
