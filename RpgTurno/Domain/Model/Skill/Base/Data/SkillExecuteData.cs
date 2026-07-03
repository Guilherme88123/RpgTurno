using Domain.Model.Entity.Units.Base;

namespace Domain.Model.Skill.Base.Data;

public record SkillExecuteData
{
    public BaseUnitEntity Sender { get; set; }
    public List<BaseUnitEntity> Targets { get; set; }
    public BaseUnitEntity Target => Targets.FirstOrDefault();

    public SkillExecuteData(BaseUnitEntity sender, List<BaseUnitEntity> targets)
    {
        Sender = sender;
        Targets = targets;
    }

    public SkillExecuteData(BaseUnitEntity sender, BaseUnitEntity target)
    {
        Sender = sender;
        Targets = [target];
    }
}
