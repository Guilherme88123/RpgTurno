using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace Domain.Model.Entity.Units.Lancer;

public class LancerEntity : BaseUnitEntity
{
    public LancerEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerAttack);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationModel(idle, 12, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationModel(running, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Defending, new AnimationModel(defending, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationModel(attack, 3, 1, 0.1f));

        SizeX = 320;
        SizeY = 320;
    }
}
