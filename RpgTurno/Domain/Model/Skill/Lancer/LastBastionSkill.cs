using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Lancer;

public class LastBastionSkill : BaseSkill
{
    public override string Name => "Last Bastion";
    public override string Description => "The final point \nbetween death \nand glory";

    public override TargetSkillType TargetType => TargetSkillType.Self;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 5;
    public override int ManaCost => 20;

    public override SkillAnimation Animation => new SkillAnimation(null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var context = new SkillContext(skillData.Sender, skillData.Target);

        skillData.Target.AddEffect(new LastBastionEffect());

        return new SkillResult(context);
    }
}
