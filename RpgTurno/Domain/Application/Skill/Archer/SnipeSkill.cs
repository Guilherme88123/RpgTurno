using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Archer;

namespace Domain.Application.Skill;

public class SnipeSkill : BaseSkill
{
    public override string Name => TextConst.Snipe;
    public override string Description => TextConst.SnipeDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 2.4f;
    public override float PowerMax => 2.8f;

    public override int Cooldown => 5;
    public override int ManaCost => 22;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new SnipeAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, damage);

        if (HasCriticalAttack(skillData.Sender, chanceMultiplier: 1.2f))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);
        skillData.Target.ApplyReciveAttackEffects(context);

        ApplySubtractTargetDefense(context, skillData.Target);

        ApplySkillAttack(skillData.Target, context);

        return new SkillResult(context);
    }
}
