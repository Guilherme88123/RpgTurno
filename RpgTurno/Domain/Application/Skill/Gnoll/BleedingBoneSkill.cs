using Domain.Application.Effect;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Archer;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;

namespace Domain.Application.Skill.Gnoll;

public class BleedingBoneSkill : BaseSkill
{
    public override string Name => TextConst.BleedingBone;
    public override string Description => TextConst.BleedingBoneDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.15f;
    public override float PowerMax => 1.35f;

    public override int Cooldown => 3;
    public override int ManaCost => 12;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new LightShootAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var result = ExecuteDefaultSingleTargetAttack(skillData);

        if (!result.Contexts.First().HasMissed)
            skillData.Target.AddEffect(new BleedEffect());

        return result;
    }
}
