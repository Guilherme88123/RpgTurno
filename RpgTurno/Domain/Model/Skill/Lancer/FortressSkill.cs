using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Sound.Attack.Lancer;

namespace Domain.Model.Skill.Lancer;

public class FortressSkill : BaseSkill
{
    public override string Name => "Fortress";
    public override string Description => "Transforms allies \ninto veritable \nfortresses";

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 4;
    public override int ManaCost => 12;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new FortressAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        List<SkillContext> contextList = new List<SkillContext>();

        foreach (var target in skillData.Targets)
        {
            var context = new SkillContext(skillData.Sender, skillData.Target);

            target.AddEffect(new FortressEffect());

            contextList.Add(context);
        }

        return new SkillResult(contextList);
    }
}
