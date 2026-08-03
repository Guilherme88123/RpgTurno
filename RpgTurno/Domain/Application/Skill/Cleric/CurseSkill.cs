using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Effect;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Cleric;
using Domain.Application.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Application.Skill.Cleric;

public class CurseSkill : BaseSkill
{
    public override string Name => TextConst.Curse;
    public override string Description => TextConst.CurseDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.5f;
    public override float PowerMax => 1.9f;

    public override int Cooldown => 4;
    public override int ManaCost => 20;

    public override SkillAnimation Animation => new SkillAnimation(new CurseSprite(), null, new CurseAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var result = ExecuteDefaultSingleTargetAttack(skillData);

        if (!result.Contexts.First().HasMissed)
            skillData.Target.AddEffect(new CurseEffect());

        return result;
    }
}
