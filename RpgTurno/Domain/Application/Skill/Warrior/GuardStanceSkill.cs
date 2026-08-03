using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Effect;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;

namespace Domain.Application.Skill.Warrior;

public class GuardStanceSkill : BaseSkill
{
    public override string Name => TextConst.GuardStance;
    public override string Description => TextConst.GuardStanceDescription;

    public override TargetSkillType TargetType => TargetSkillType.Self;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 3;
    public override int ManaCost => 10;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new GuardStanceSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var context = new SkillContext(skillData.Sender, skillData.Target);

        skillData.Target.AddEffect(new GuardStanceEffect());

        return new SkillResult(context);
    }
}
