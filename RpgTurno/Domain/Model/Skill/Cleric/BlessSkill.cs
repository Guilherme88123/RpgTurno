using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Model.Skill;

public class BlessSkill : BaseSkill
{
    public override string Name => "Bless";
    public override string Description => "Applies the \nBlessing of Bravery \nto an ally";

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Stats;

    public override float PowerMin => 0f;
    public override float PowerMax => 0f;

    public override int Cooldown => 3;
    public override int ManaCost => 10;

    public override SkillAnimation Animation => new SkillAnimation(new HealSprite(), null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        skillData.Target.AddEffect(new BraveryBlessEffect());

        return new SkillResult(new SkillContext(skillData.Sender, skillData.Target));
    }
}
