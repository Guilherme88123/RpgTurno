using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill;

public class PowerShootSkill : BaseSkill
{
    public override string Name => "Power Shoot";
    public override string Description => "A more powerful \nrange attack";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.3f;
    public override float PowerMax => 1.6f;

    public override int Cooldown => 2;
    public override int ManaCost => 8;

    public override SkillAnimation Animation => new SkillAnimation(null, null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
