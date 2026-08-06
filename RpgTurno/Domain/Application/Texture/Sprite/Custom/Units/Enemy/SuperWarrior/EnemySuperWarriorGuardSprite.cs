using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.SuperWarrior;

public class EnemySuperWarriorGuardSprite : AnimationClip
{
    public EnemySuperWarriorGuardSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorGuard), 
        framesX: 6, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1)
    {
    }
}
