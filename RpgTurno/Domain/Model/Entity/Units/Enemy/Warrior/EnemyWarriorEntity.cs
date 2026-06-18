using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Warrior;

public class EnemyWarriorEntity : BaseUnitEntity
{
    public EnemyWarriorEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorAttack);

        var spriteBorder = new BorderDefinition(48, 48, 48, 48);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationClip(idle, 8, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Defending, new AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationClip(attack, 4, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
    }
}
