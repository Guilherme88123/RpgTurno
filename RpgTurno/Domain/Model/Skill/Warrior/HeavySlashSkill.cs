using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Warrior;

public class HeavySlashSkill : BaseSkill
{
    public override string Name => "Heavy Slash";
    public override string Description => "A heavy \nand more \npowerful attack";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.7f;
    public override float PowerMax => 2.0f;

    public override int Cooldown => 2;
    public override int ManaCost => 8;

    public override SkillAnimation Animation => new SkillAnimation(null, null, false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
