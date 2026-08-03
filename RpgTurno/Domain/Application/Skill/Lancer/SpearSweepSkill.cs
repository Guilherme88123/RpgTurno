using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Lancer;

namespace Domain.Application.Skill.Lancer;

public class SpearSweepSkill : BaseSkill
{
    public override string Name => TextConst.SpearSweep;
    public override string Description => TextConst.SpearSweepDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.8f;
    public override float PowerMax => 0.9f;

    public override int Cooldown => 3;
    public override int ManaCost => 14;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new SwearSweepAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultMultipleTargetAttack(skillData);
    }
}
