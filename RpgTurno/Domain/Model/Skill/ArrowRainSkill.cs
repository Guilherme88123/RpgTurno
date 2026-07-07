using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill;

public class ArrowRainSkill : BaseSkill
{
    public override string Name => "Arrow Rain";
    public override string Description => "Attack all targets with porwerfull arrows.";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.55f;
    public override float PowerMax => 0.9f;

    public override int Cooldown => 2;

    public override SkillAnimation Animation => new SkillAnimation(null, null, true, 1.0f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        foreach (var target in skillData.Targets)
            target.RecieveAttack(damage);

        return new SkillResult(skillData.Sender, skillData.Target, damage);
    }
}
