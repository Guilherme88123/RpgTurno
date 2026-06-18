using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Ally.Warrior;

public class WarriorEntity : BaseUnitEntity
{
    public WarriorEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorAttack);

        var spriteBorder = new BorderDefinition(48, 48, 48, 48);

        Animation.AddAnimation(CreatureStateType.Idle, new Animation.AnimationClip(idle, 8, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Running, new Animation.AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Defending, new Animation.AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Attacking, new Animation.AnimationClip(attack, 4, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
    }
}
