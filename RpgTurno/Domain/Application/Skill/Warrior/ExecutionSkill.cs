using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;

namespace Domain.Application.Skill.Warrior;

public class ExecutionSkill : BaseSkill
{
    public override string Name => TextConst.Execution;
    public override string Description => TextConst.ExecutionDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 2.8f;
    public override float PowerMax => 3.0f;

    public override int Cooldown => 5;
    public override int ManaCost => 20;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new ExecutionSwordAttackSoundEffect(), false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, damage);

        if (HasCriticalAttack(skillData.Sender))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);
        skillData.Target.ApplyReciveAttackEffects(context);

        ApplySubtractTargetDefense(context, skillData.Target);

        ApplySkillAttack(skillData.Target, context);

        return new SkillResult(context);
    }
}
