using Domain.Const.Sprite;

namespace Domain.Model.Texture.Sprite.Custom.Terrain.Trees;

public class Tree2Sprite : AnimationClip
{
    public Tree2Sprite() : base(
        SpriteConst.Tree2,
        framesX: 8,
        framesY: 1,
        frameTime: 0.1f,
        row: 1,
        border: null)
    {
    }
}
