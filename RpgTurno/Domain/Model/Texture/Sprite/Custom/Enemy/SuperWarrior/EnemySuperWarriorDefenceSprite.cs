using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.ParticleFx;

public class EnemySuperWarriorDefenceSprite : AnimationClip
{
    public EnemySuperWarriorDefenceSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemySuperWarriorDefence), 
        framesX: 6, 
        framesY: 1, 
        frameTime: 0.1f, 
        row: 1)
    {
    }
}
