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

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationClip(idle, 12, 1, 0.1f, borderHorizontal: 112, borderVertical: 48));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, borderHorizontal: 112, borderVertical: 48));
        Animation.AddAnimation(CreatureStateType.Defending, new AnimationClip(defending, 6, 1, 0.1f, borderHorizontal: 112, borderVertical: 48));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationClip(attack, 3, 1, 0.1f, borderHorizontal: 112, borderVertical: 48));

        SizeX = 96;
        SizeY = 224;
    }
}
