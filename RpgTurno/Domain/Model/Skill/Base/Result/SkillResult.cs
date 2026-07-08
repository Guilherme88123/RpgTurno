using Domain.Model.Entity.Units.Base;

namespace Domain.Model.Skill.Base.Result;

public record SkillResult
{
    public List<SkillContext> Contexts { get; set; }
    public List<BaseUnitEntity> Targets => Contexts.Select(x => x.Target).ToList();

    public SkillResult(SkillContext context)
    {
        Contexts = [context];
    }

    public SkillResult(List<SkillContext> contexts)
    {
        Contexts = contexts;
    }
}
