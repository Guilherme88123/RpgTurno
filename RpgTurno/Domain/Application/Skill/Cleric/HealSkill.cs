using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Cleric;
using Domain.Application.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Application.Skill.Cleric;

public class HealSkill : BaseSkill
{
    public override string Name => TextConst.Heal;
    public override string Description => TextConst.HealDescription;

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Heal;

    public override float PowerMin => 1.1f;
    public override float PowerMax => 1.35f;

    public override int Cooldown => 1;
    public override int ManaCost => 8;

    public override SkillAnimation Animation => new SkillAnimation(new HealSprite(), null, new HealAttackSoundEffect(), true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var healAmount = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, healAmount);

        if (HasCriticalAttack(skillData.Sender))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);

        skillData.Target.RecieveHeal(healAmount, context.HasCritical);

        return new SkillResult(context);
    }
}
