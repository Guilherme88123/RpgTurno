using Domain.Model.Entity.Units.Base;

namespace Domain.Model.Skill.Base.Result;

public record SkillContext
{
    public BaseUnitEntity Sender { get; set; }
    public BaseUnitEntity Target { get; set; }
    public int Value { get; set; }
    public bool HasMissed { get; set; }
    public bool HasCritical { get; set; }

    public SkillContext(BaseUnitEntity sender, BaseUnitEntity target, int value, bool hasMissed = false, bool hasCritical = false)
    {
        Sender = sender;
        Target = target;
        Value = value;
        HasMissed = hasMissed;
        HasCritical = hasCritical;
    }

    public SkillContext(BaseUnitEntity sender, BaseUnitEntity target, bool hasMissed = false, bool hasCritical = false)
    {
        Sender = sender;
        Target = target;
        HasMissed = hasMissed;
        HasCritical = hasCritical;
    }
}
