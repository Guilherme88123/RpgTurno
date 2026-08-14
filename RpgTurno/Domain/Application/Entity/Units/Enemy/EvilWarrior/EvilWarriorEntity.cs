using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Skill.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.EvilWarrior;

public class EvilWarriorEntity : BaseUnitEntity
{
    private int _attackVariation = 1;

    public EvilWarriorEntity(int level = 1) : base(
        stats: new EvilWarriorStats(level), 
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EvilWarriorIdleSprite());
        Animation.Add(CreatureStateType.Run, new EvilWarriorRunSprite());
        Animation.Add(CreatureStateType.Guard, new EvilWarriorGuardSprite());
        Animation.Add((CreatureStateType.Attack, 1), new EvilWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, 2), new EvilWarriorAttack2Sprite());

        Icon = new EvilWarriorAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.EvilWarriorUnit;
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
