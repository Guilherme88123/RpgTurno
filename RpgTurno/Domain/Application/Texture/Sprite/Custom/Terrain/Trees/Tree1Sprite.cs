using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Terrain.Trees;

public class Tree1Sprite : AnimationClip
{
    public Tree1Sprite() : base(
        SpriteConst.Tree1, 
        framesX: 8, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1, 
        border: null)
    {
    }
}
