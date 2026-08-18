using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Lancer;

namespace Domain.Application.Skill.Lancer;

public class PiercingStrikeSkill : BaseSkill
{
    public override string Name => TextConst.PiercingStrike;
    public override string Description => TextConst.PiercingStrikeDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.3f;
    public override float PowerMax => 1.5f;

    public override int Cooldown => 2;
    public override int ManaCost => 6;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new PiercingStrikeAttackSoundEffect(), false);

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
