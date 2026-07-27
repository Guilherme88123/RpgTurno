using Domain.Const.Sprite;

namespace Domain.Model.Texture.Sprite.Custom.Terrain.Trees;

public class Tree3Sprite : AnimationClip
{
    public Tree3Sprite() : base(
        SpriteConst.Tree3,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
