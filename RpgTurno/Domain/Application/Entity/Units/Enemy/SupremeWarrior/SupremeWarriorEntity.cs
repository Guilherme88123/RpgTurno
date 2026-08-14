using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Skill.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.SupremeWarrior;

public class SupremeWarriorEntity : BaseUnitEntity
{
    private int _attackVariation = 1;

    public SupremeWarriorEntity(int level = 1) : base(
        stats: new SupremeWarriorStats(level),
        skillTree: new SupremeWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SupremeWarriorIdleSprite());
        Animation.Add(CreatureStateType.Run, new SupremeWarriorRunSprite());
        Animation.Add(CreatureStateType.Guard, new SupremeWarriorGuardSprite());
        Animation.Add((CreatureStateType.Attack, 1), new SupremeWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, 2), new SupremeWarriorAttack2Sprite());

        Icon = new SupremeWarriorAvatarSprite();

        SizeX = 144;
        SizeY = 144;

        AnimationSizeX = 294;
        AnimationSizeY = 294;

        Name = TextConst.SupremeWarriorUnit;
    }

    protected override void UpdateAnimation()
    {
        if (HasGuardStanceEffect() && CreatureState == CreatureStateType.Idle)
        {
            Animation.Update(CreatureStateType.Guard);
            return;
        }

        if (CreatureState == CreatureStateType.Attack)
        {
            Animation.Update((CreatureState, _attackVariation));
            return;
        }

        base.UpdateAnimation();
    }

    private bool HasGuardStanceEffect()
    {
        return Effects.Any(x => x.Effect is GuardStanceEffect);
    }

    public override void BeforeSkillExecute(BaseSkill skill)
    {
        _attackVariation = GetRandomAttackVariation();
    }

    private int GetRandomAttackVariation()
    {
        return Random.Shared.Next(1, 3);
    }
}
