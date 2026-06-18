using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Cleric;

public class EnemyClericEntity : BaseUnitEntity
{
    public EnemyClericEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAttack);

        var spriteBorder = new BorderDefinition(48, 48, 48, 48);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationClip(attack, 11, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
    }
}
