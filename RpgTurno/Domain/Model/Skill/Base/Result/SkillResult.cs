using Domain.Model.Entity.Units.Base;

namespace Domain.Model.Skill.Base.Result;

public record SkillResult
{
    public BaseUnitEntity Sender { get; set; }
    public List<BaseUnitEntity> Targets { get; set; }
    public int Value { get; set; }

    public SkillResult(BaseUnitEntity sender, List<BaseUnitEntity> targets, int damage)
    {
        Sender = sender;
        Targets = targets;
        Value = damage;
    }

    public SkillResult(BaseUnitEntity sender, BaseUnitEntity target, int damage)
    {
        Sender = sender;
        Targets = [target];
        Value = damage;
    }
}
