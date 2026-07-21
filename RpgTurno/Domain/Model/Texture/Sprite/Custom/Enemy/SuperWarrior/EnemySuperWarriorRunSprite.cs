using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class EnemySuperWarriorRunSprite : AnimationClip
{
    public EnemySuperWarriorRunSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorRun), 
        framesX: 6, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1)
    {
    }
}
