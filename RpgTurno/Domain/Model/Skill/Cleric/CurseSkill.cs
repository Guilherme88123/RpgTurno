using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Model.Skill.Cleric;

public class CurseSkill : BaseSkill
{
    public override string Name => "Curse";
    public override string Description => "A colossal attack \nthat makes enemies \nregret it";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.5f;
    public override float PowerMax => 1.9f;

    public override int Cooldown => 4;
    public override int ManaCost => 20;

    public override SkillAnimation Animation => new SkillAnimation(new CurseSprite(), null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var result = ExecuteDefaultSingleTargetAttack(skillData);

        if (!result.Contexts.First().HasMissed)
            skillData.Target.AddEffect(new CurseEffect());

        return result;
    }
}
