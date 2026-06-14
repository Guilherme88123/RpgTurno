using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Cleric;

public class EnemyClericEntity : BaseUnitEntity
{
    public EnemyClericEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAttack);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationModel(idle, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationModel(running, 4, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationModel(attack, 11, 1, 0.1f));

        SizeX = 192;
        SizeY = 192;
    }
}
