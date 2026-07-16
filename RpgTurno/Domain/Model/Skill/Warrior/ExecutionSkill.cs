using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Warrior;

public class ExecutionSkill : BaseSkill
{
    public override string Name => "Execution";
    public override string Description => "Executes an enemy \nwith catastrophic \ndamage";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 2.8f;
    public override float PowerMax => 3.0f;

    public override int Cooldown => 5;
    public override int ManaCost => 20;

    public override SkillAnimation Animation => new SkillAnimation(null, null, false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
