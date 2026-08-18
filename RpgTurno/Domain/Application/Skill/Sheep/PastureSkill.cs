using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Cleric;
using Domain.Application.Texture.Sprite.Custom.ParticleFx;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;

namespace Domain.Application.Skill.Sheep;

public class PastureSkill : BaseSkill
{
    public override string Name => TextConst.Pasture;
    public override string Description => TextConst.PastureDescription;

    public override TargetSkillType TargetType => TargetSkillType.Self;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Heal;

    public override float PowerMin => 1.0f;
    public override float PowerMax => 1.2f;

    public override int Cooldown => 2;
    public override int ManaCost => 12;

    public override SkillAnimation Animation => new SkillAnimation(new HealSprite(), null, new HealAttackSoundEffect(), true);

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
