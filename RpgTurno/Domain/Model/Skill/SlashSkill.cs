using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill;

public class SlashSkill : BaseSkill
{
    public override string Name => "Slash";
    public override string Description => "A powerful \nslash attack";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.8f;
    public override float PowerMax => 1.2f;

    public override int Cooldown => 0;
    public override int ManaCost => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, damage);

        skillData.Sender.ApplyExecuteAttackEffects(context);

        skillData.Target.ApplyReciveAttackEffects(context);

        skillData.Target.RecieveAttack(damage);

        return new SkillResult(context);
    }
}
