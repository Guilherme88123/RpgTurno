using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Sound.Attack.Warrior;

namespace Domain.Model.Skill.Warrior;

public class CleaveSkill : BaseSkill
{
    public override string Name => "Cleave";
    public override string Description => "Attacks all enemies \nwith a horizontal \nslash.";

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
