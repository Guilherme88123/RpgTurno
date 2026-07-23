using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Sound.Attack.Archer;

namespace Domain.Model.Skill;

public class PoisonShootSkill : BaseSkill
{
    public override string Name => "Poison Shoot";
    public override string Description => "A poisonous attack \nthat infects its \ntarget";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.2f;
    public override float PowerMax => 1.45f;

    public override int Cooldown => 3;
    public override int ManaCost => 10;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new LightShootAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var result = ExecuteDefaultSingleTargetAttack(skillData);

        if (!result.Contexts.First().HasMissed)
            skillData.Target.AddEffect(new PoisonEffect());

        return result;
    }
}
