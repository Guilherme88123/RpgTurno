using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite.CustomSprites;

namespace Domain.Model.Skill;

public class RageSkill : BaseSkill
{
    public override string Name => "Rage";
    public override string Description => "This unit is \novercome by rage";

    public override TargetSkillType TargetType => TargetSkillType.Self;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 3;

    public override SkillAnimation Animation => new SkillAnimation(null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var context = new SkillContext(skillData.Sender, skillData.Target);

        skillData.Target.AddEffect(new RageEffect());

        return new SkillResult(context);
    }
}
