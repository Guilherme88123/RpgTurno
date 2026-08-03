using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Terrain.Bushes;

public class Bushe3Sprite : AnimationClip
{
    public Bushe3Sprite() : base(
        SpriteConst.Bushe3,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
