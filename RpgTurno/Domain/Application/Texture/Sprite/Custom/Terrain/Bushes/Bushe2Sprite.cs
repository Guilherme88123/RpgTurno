using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Terrain.Bushes;

public class Bushe2Sprite : AnimationClip
{
    public Bushe2Sprite() : base(
        SpriteConst.Bushe2,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
