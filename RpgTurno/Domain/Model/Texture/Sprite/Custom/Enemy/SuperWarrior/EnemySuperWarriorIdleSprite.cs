using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class EnemySuperWarriorIdleSprite : AnimationClip
{
    public EnemySuperWarriorIdleSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorIdle), 
        framesX: 8, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1)
    {
    }
}
