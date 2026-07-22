using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using System.Reflection;

namespace Domain.Model.Skill.Archer;

public class ArrowRainSkill : BaseSkill
{
    public override string Name => "Arrow Rain";
    public override string Description => "Attack all \ntargets with \nporwerfull arrows";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.55f;
    public override float PowerMax => 0.9f;

    public override int Cooldown => 4;
    public override int ManaCost => 18;

    public override SkillAnimation Animation => new SkillAnimation(null, null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultMultipleTargetAttack(skillData);
    }
}
