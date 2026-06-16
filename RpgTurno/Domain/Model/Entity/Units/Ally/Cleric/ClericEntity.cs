using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Ally.Cleric;

public class ClericEntity : BaseUnitEntity
{
    public ClericEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ClericIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ClericRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ClericAttack);

        Animation.AddAnimation(CreatureStateType.Idle, new Animation.Animation(idle, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Running, new Animation.Animation(running, 4, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Attacking, new Animation.Animation(attack, 11, 1, 0.1f));

        SizeX = 192;
        SizeY = 192;
    }
}
