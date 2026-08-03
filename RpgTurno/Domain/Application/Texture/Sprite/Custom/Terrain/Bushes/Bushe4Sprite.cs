using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Terrain.Bushes;

public class Bushe4Sprite : AnimationClip
{
    public Bushe4Sprite() : base(
        SpriteConst.Bushe4,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
