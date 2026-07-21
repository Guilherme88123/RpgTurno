using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Warrior;

public class GuardStanceSkill : BaseSkill
{
    public override string Name => "Guard Stance";
    public override string Description => "Place this unit \nin a guard stance.";

    public override TargetSkillType TargetType => TargetSkillType.Self;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 3;
    public override int ManaCost => 10;

    public override SkillAnimation Animation => new SkillAnimation(null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var context = new SkillContext(skillData.Sender, skillData.Target);

        skillData.Target.AddEffect(new GuardStanceEffect());

        return new SkillResult(context);
    }
}
