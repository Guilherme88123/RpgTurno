using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Warrior;

public class WarriorEntity : BaseUnitEntity
{
    public WarriorEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorAttack);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationModel(idle, 8, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationModel(running, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Defending, new AnimationModel(defending, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationModel(attack, 4, 1, 0.1f));

        SizeX = 192;
        SizeY = 192;
    }
}
