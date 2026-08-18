using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;

namespace Domain.Application.Skill.Warrior;

public class HeavySlashSkill : BaseSkill
{
    public override string Name => TextConst.HeavySlash;
    public override string Description => TextConst.HeavySlashDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.7f;
    public override float PowerMax => 2.0f;

    public override int Cooldown => 2;
    public override int ManaCost => 8;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new HeavySwordAttackSoundEffect(), false);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
