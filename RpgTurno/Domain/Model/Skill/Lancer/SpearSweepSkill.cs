using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Lancer;

public class SpearSweepSkill : BaseSkill
{
    public override string Name => "Spear Sweep";
    public override string Description => "Performs a sweep \nwith their spear";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.8f;
    public override float PowerMax => 0.9f;

    public override int Cooldown => 3;
    public override int ManaCost => 14;

    public override SkillAnimation Animation => new SkillAnimation(null, null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultMultipleTargetAttack(skillData);
    }
}
