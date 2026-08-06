using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SuperWarrior;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.SuperWarrior;

public class EnemySuperWarriorEntity : BaseUnitEntity
{
    public EnemySuperWarriorEntity(int level = 20) : base(stats: new EnemySuperWarriorStats(level), skillTree: new EnemySuperWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EnemySuperWarriorIdleSprite());
        Animation.Add(CreatureStateType.Running, new EnemySuperWarriorRunSprite());
        Animation.Add(CreatureStateType.Defending, new EnemySuperWarriorGuardSprite());
        Animation.Add(CreatureStateType.Attacking, new EnemySuperWarriorAttackingSprite());

        SizeX = 144;
        SizeY = 144;
        Name = TextConst.SupremeWarriorUnit;

        AnimationSizeX = 294;
        AnimationSizeY = 294;

        Icon = new EnemySuperWarriorAvatarSprite();
    }

    protected override void UpdateAnimation()
    {
        if (HasGuardStanceEffect() && CreatureState == CreatureStateType.Idle)
        {
            Animation.Update(CreatureStateType.Defending);
            return;
        }

        base.UpdateAnimation();
    }

    private bool HasGuardStanceEffect()
    {
        return Effects.Any(x => x.Effect is GuardStanceEffect);
    }
}
