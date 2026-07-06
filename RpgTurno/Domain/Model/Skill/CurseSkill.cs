using Domain.Enum.Skill.Target;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill;

public class CurseSkill : BaseSkill
{
    public override string Name => "Curse";
    public override string Description => "A red curse attack.";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;

    public override float PowerMin => 0.85f;
    public override float PowerMax => 1.15f;

    public override int Cooldown => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, true, 1.0f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        skillData.Target.RecieveAttack(damage);

        return new SkillResult(skillData.Sender, skillData.Target, damage);
    }
}
