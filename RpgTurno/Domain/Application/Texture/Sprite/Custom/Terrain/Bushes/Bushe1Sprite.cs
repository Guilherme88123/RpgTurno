using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Terrain.Bushes;

public class Bushe1Sprite : AnimationClip
{
    public Bushe1Sprite() : base(
        SpriteConst.Bushe1,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
