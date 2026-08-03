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

public class DivineLightSkill : BaseSkill
{
    public override string Name => TextConst.DivineLight;
    public override string Description => TextConst.DivineLightDescription;

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Stats;

    public override float PowerMin => 1.4f;
    public override float PowerMax => 1.85f;

    public override int Cooldown => 5;
    public override int ManaCost => 24;

    public override SkillAnimation Animation => new SkillAnimation(new HealSprite(), null, new DivineLightAttackSoundEffect(), true, 0.5f);

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
