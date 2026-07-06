using Domain.Enum.Skill.Target;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill;

public class SlashSkill : BaseSkill
{
    public override string Name => "Slash";
    public override string Description => "A powerful slash attack.";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;

    public override float PowerMin => 0.8f;
    public override float PowerMax => 1.2f;

    public override int Cooldown => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, false, 1.0f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        skillData.Target.RecieveAttack(damage);

        return new SkillResult(skillData.Sender, skillData.Target, damage);
    }
}
