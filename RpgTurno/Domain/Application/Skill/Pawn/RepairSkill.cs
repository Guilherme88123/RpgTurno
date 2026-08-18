using Domain.Application.Effect;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Cleric;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;

namespace Domain.Application.Skill.Pawn;

public class RepairSkill : BaseSkill
{
    public override string Name => TextConst.Repair;
    public override string Description => TextConst.RepairDescription;

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Heal;

    public override float PowerMin => 0.80f;
    public override float PowerMax => 1.05f;

    public override int Cooldown => 4;
    public override int ManaCost => 14;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new HealAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var healAmount = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, healAmount);

        skillData.Target.AddEffect(new RepairedEffect());

        if (HasCriticalAttack(skillData.Sender))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);

        skillData.Target.RecieveHeal(healAmount, context.HasCritical);

        return new SkillResult(context);
    }
}
