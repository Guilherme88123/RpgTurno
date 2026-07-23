using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Sound.Attack.Lancer;

namespace Domain.Model.Skill.Lancer;

public class PikeSkill : BaseSkill
{
    public override string Name => "Pike";
    public override string Description => "A pike attack";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.9f;
    public override float PowerMax => 1.1f;

    public override int Cooldown => 0;
    public override int ManaCost => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new PikeAttackSoundEffect(), false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }
}
