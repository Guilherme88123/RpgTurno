using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Lancer;

public class PiercingStrikeSkill : BaseSkill
{
    public override string Name => "Piercing \nStrike";
    public override string Description => "A piercing attack \nthat ignores the \ntarget's armor";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.3f;
    public override float PowerMax => 1.5f;

    public override int Cooldown => 2;
    public override int ManaCost => 6;

    public override SkillAnimation Animation => new SkillAnimation(null, null, false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, damage);

        if (!HasHitAttack(skillData.Sender, skillData.Target))
        {
            var missContext = new SkillContext(skillData.Sender, skillData.Target, hasMissed: true);

            ApplySkillAttack(skillData.Target, missContext);

            return new SkillResult(missContext);
        }

        if (HasCriticalAttack(skillData.Sender))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);
        skillData.Target.ApplyReciveAttackEffects(context);

        ApplySkillAttack(skillData.Target, context);

        return new SkillResult(context);
    }
}
