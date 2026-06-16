using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Lancer;

public class EnemyLancerEntity : BaseUnitEntity
{
    public EnemyLancerEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerAttack);

        Animation.AddAnimation(CreatureStateType.Idle, new Animation.Animation(idle, 12, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Running, new Animation.Animation(running, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Defending, new Animation.Animation(defending, 6, 1, 0.1f));
        Animation.AddAnimation(CreatureStateType.Attacking, new Animation.Animation(attack, 3, 1, 0.1f));

        SizeX = 320;
        SizeY = 320;
    }
}
