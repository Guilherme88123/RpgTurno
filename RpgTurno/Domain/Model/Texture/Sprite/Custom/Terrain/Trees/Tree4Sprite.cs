using Domain.Const.Sprite;

namespace Domain.Model.Texture.Sprite.Custom.Terrain.Trees;

public class Tree4Sprite : AnimationClip
{
    public Tree4Sprite() : base(
        SpriteConst.Tree4,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
