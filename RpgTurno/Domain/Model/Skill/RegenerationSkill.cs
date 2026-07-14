using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite.CustomSprites;

namespace Domain.Model.Skill;

public class RegenerationSkill : BaseSkill
{
    public override string Name => "Regeneration";
    public override string Description => "A skill that \nblesses your allies \nwith health points";

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 4;
    public override int ManaCost => 28;

    public override SkillAnimation Animation => new SkillAnimation(new HealAnimation(), null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        List<SkillContext> contextList = new List<SkillContext>();

        foreach (var target in skillData.Targets)
        {
            var context = new SkillContext(skillData.Sender, target);

            target.AddEffect(new RegenerationEffect());

            contextList.Add(context);
        }

        return new SkillResult(contextList);
    }
}
