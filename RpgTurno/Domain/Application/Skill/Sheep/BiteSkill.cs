using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;

namespace Domain.Application.Skill.Sheep;

public class BiteSkill : BaseSkill
{
    public override string Name => TextConst.Bite;
    public override string Description => TextConst.BiteDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.0f;
    public override float PowerMax => 1.15f;

    public override int Cooldown => 0;
    public override int ManaCost => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new SwordAttackSoundEffect(), false);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
