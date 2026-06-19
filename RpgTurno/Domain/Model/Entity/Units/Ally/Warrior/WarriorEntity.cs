using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
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

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 8, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Defending, new AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 4, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
    }
}
