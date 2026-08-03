using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.ParticleFx;

public class EnemySuperWarriorAttackingSprite : AnimationClip
{
    public EnemySuperWarriorAttackingSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorAttack), 
        framesX: 4, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1)
    {
    }
}
