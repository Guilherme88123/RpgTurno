using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Archer;

public class EnemyArcherEntity : BaseUnitEntity
{
    public EnemyArcherEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherAttack);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, borderHorizontal: 48, borderVertical: 48));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationClip(running, 4, 1, 0.1f, borderHorizontal: 48, borderVertical: 48));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationClip(attack, 8, 1, 0.1f, borderHorizontal: 48, borderVertical: 48));

        SizeX = 96;
        SizeY = 96;
    }
}
