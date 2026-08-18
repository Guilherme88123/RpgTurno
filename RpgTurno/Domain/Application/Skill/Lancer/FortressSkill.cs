using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Effect;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Lancer;

namespace Domain.Application.Skill.Lancer;

public class FortressSkill : BaseSkill
{
    public override string Name => TextConst.Fortress;
    public override string Description => TextConst.FortressDescription;

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Stats;

    public override int Cooldown => 4;
    public override int ManaCost => 12;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new FortressAttackSoundEffect(), true);

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
