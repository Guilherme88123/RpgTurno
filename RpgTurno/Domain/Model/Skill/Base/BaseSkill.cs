using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Base;

public abstract class BaseSkill
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract TargetSkillType TargetType { get; }
    public abstract TargetSkillAmount TargetAmount { get; }
    public abstract SkillType Type { get; }

    public virtual float PowerMin => 0;
    public virtual float PowerMax => 0;

    public abstract int Cooldown { get; }
    public abstract int ManaCost { get; }

    public abstract SkillResult ExecuteSkill(SkillExecuteData skillData);

    public abstract SkillAnimation Animation { get; }

    protected float GetRandomMultiplier()
    {
        return Random.Shared.NextSingle() * (PowerMax - PowerMin) + PowerMin;
    }

    protected int CalculateValue(SkillExecuteData data)
    {
        return (int)(data.Sender.Stats.Attack * GetRandomMultiplier());
    }

    protected void ApplySubtractTargetDefense(SkillContext context, BaseUnitEntity target)
    {
        float reduction = 100f / (100f + target.Stats.Defense);

        context.Value = (int)MathF.Max(1, context.Value * reduction);
    }

    #region Miss

    protected bool HasHitAttack(BaseUnitEntity sender, BaseUnitEntity target)
    {
        var chance = CalculateHitAttackChance(sender, target);
        return HasSuccessByChance(chance);
    }

    private int CalculateHitAttackChance(BaseUnitEntity sender, BaseUnitEntity target)
    {
        var senderAccuracy = sender.Stats.Accuracy;
        var targetEvasion = target.Stats.Evasion;

        var rawChance = senderAccuracy - targetEvasion;

        return Math.Clamp(rawChance, 5, 99);
    }

    #endregion

    #region Critical

    protected bool HasCriticalAttack(BaseUnitEntity sender, float chanceMultiplier = 1.0f)
    {
        var chance = CalculateCriticalAttackChance(sender, chanceMultiplier);

        return HasSuccessByChance(chance);
    }

    private int CalculateCriticalAttackChance(BaseUnitEntity sender, float chanceMultiplier)
    {
        return (int)(sender.Stats.CriticalChance * chanceMultiplier);
    }

    protected void ApplyCriticalModifier(SkillContext context, BaseUnitEntity sender)
    {
        context.Value = (int)(context.Value * sender.Stats.CriticalDamage);
        context.HasCritical = true;
    }

    #endregion

    private bool HasSuccessByChance(int chance)
    {
        return Random.Shared.Next(100) < chance;
    }

    #region Apply Attack

    protected void ApplySkillAttack(BaseUnitEntity target, SkillContext context)
    {
        target.RecieveAttack(context.Value, context.HasMissed, context.HasCritical);
    }

    #endregion

    #region Default Parterns

    protected SkillResult ExecuteDefaultSingleTargetAttack(SkillExecuteData skillData)
    {
        var damage = CalculateValue(skillData);

        var context = new SkillContext(skillData.Sender, skillData.Target, damage);

        if (!HasHitAttack(skillData.Sender, skillData.Target))
        {
            var missContext = new SkillContext(skillData.Sender, skillData.Target, hasMissed: true);

            ApplySkillAttack(skillData.Target, missContext);

            return new SkillResult(missContext);
        }

        if (HasCriticalAttack(skillData.Sender))
            ApplyCriticalModifier(context, skillData.Sender);

        skillData.Sender.ApplyExecuteAttackEffects(context);
        skillData.Target.ApplyReciveAttackEffects(context);

        ApplySubtractTargetDefense(context, skillData.Target);

        ApplySkillAttack(skillData.Target, context);

        return new SkillResult(context);
    }

    protected SkillResult ExecuteDefaultMultipleTargetAttack(SkillExecuteData skillData)
    {
        List<SkillContext> contextList = new List<SkillContext>();

        foreach (var target in skillData.Targets)
        {
            var damage = CalculateValue(skillData);

            var context = new SkillContext(skillData.Sender, target, damage);

            if (!HasHitAttack(skillData.Sender, target))
            {
                var missContext = new SkillContext(skillData.Sender, target, hasMissed: true);

                ApplySkillAttack(target, missContext);

                contextList.Add(missContext);

                continue;
            }

            if (HasCriticalAttack(skillData.Sender))
                ApplyCriticalModifier(context, skillData.Sender);

            skillData.Sender.ApplyExecuteAttackEffects(context);
            target.ApplyReciveAttackEffects(context);

            ApplySubtractTargetDefense(context, target);

            ApplySkillAttack(target, context);

            contextList.Add(context);
        }

        return new SkillResult(contextList);
    }

    #endregion
}
