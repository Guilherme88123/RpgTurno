using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Cleric;

namespace Domain.Application.Skill.Cleric;

public class SmiteSkill : BaseSkill
{
    public override string Name => TextConst.Smite;
    public override string Description => TextConst.SmiteDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.0f;
    public override float PowerMax => 1.15f;

    public override int Cooldown => 0;
    public override int ManaCost => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new SmiteAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
