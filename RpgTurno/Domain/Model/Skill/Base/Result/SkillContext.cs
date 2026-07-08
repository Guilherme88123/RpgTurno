using Domain.Model.Entity.Units.Base;

namespace Domain.Model.Skill.Base.Result;

public record SkillContext
{
    public BaseUnitEntity Sender { get; set; }
    public BaseUnitEntity Target { get; set; }
    public int Value { get; set; }

    public SkillContext(BaseUnitEntity sender, BaseUnitEntity target, int value)
    {
        Sender = sender;
        Target = target;
        Value = value;
    }

    public SkillContext(BaseUnitEntity sender, BaseUnitEntity target)
    {
        Sender = sender;
        Target = target;
    }
}
