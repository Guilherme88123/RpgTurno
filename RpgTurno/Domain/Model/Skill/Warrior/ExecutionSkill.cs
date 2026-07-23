using Domain.Const.Sound.Effect;
using Domain.Dto.Global;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Sound;
using Microsoft.Xna.Framework.Audio;

namespace Domain.Model.Skill.Warrior;

public class ExecutionSkill : BaseSkill
{
    public override string Name => "Execution";
    public override string Description => "Executes an enemy \nwith catastrophic \ndamage";

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 2.8f;
    public override float PowerMax => 3.0f;

    public override int Cooldown => 5;
    public override int ManaCost => 20;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new ExecutionSwordAttackSoundEffect(), false, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, damage);

        if (HasCriticalAttack(skillData.Sender))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);
        skillData.Target.ApplyReciveAttackEffects(context);

        ApplySubtractTargetDefense(context, skillData.Target);

        ApplySkillAttack(skillData.Target, context);

        return new SkillResult(context);
    }
}
