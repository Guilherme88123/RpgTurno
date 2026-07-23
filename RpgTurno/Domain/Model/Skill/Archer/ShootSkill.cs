using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Sound.Attack.Archer;

namespace Domain.Model.Skill.Archer;

public class ShootSkill : BaseSkill
{
    public override string Name => "Shoot";
    public override string Description => "A ranged bow attack";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.8f;
    public override float PowerMax => 1.2f;

    public override int Cooldown => 0;
    public override int ManaCost => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new ShootAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
