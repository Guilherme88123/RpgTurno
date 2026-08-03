using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;

namespace Domain.Application.Skill.Warrior;

public class CleaveSkill : BaseSkill
{
    public override string Name => TextConst.Cleave;
    public override string Description => TextConst.CleaveDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.75f;
    public override float PowerMax => 0.85f;

    public override int Cooldown => 3;
    public override int ManaCost => 14;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new CleaveSwordAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultMultipleTargetAttack(skillData);
    }
}
