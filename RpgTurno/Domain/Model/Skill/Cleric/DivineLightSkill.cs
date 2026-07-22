using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Effect;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Model.Skill.Cleric;

public class DivineLightSkill : BaseSkill
{
    public override string Name => "Divine Light";
    public override string Description => "It carries the \ndivine light as \na companion";

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Stats;

    public override float PowerMin => 1.4f;
    public override float PowerMax => 1.85f;

    public override int Cooldown => 5;
    public override int ManaCost => 24;

    public override SkillAnimation Animation => new SkillAnimation(new HealSprite(), null, null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        List<SkillContext> contextList = new List<SkillContext>();

        foreach (var target in skillData.Targets)
        {
            var healAmount = CalculateValue(skillData);

            var context = new SkillContext(skillData.Sender, skillData.Target, healAmount);

            target.AddEffect(new RegenerationEffect());

            if (HasCriticalAttack(skillData.Sender))
                ApplyCriticalModifier(context, skillData.Sender);

            skillData.Sender.ApplyExecuteAttackEffects(context);

            target.RecieveHeal(healAmount, context.HasCritical);

            contextList.Add(context);
        }

        return new SkillResult(contextList);
    }
}
