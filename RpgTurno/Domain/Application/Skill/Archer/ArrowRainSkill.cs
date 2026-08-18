using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Archer;

namespace Domain.Application.Skill.Archer;

public class ArrowRainSkill : BaseSkill
{
    public override string Name => TextConst.ArrowRain;
    public override string Description => TextConst.ArrowRainDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.55f;
    public override float PowerMax => 0.9f;

    public override int Cooldown => 4;
    public override int ManaCost => 18;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new ArrowRainAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultMultipleTargetAttack(skillData);
    }
}
