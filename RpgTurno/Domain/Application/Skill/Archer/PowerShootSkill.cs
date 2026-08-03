using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Archer;

namespace Domain.Application.Skill;

public class PowerShootSkill : BaseSkill
{
    public override string Name => TextConst.PowerShoot;
    public override string Description => TextConst.PowerShootDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.3f;
    public override float PowerMax => 1.6f;

    public override int Cooldown => 2;
    public override int ManaCost => 8;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new PowerShootSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
