using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Base.Skill.Definition;

public record UnitSkillDefinition
{
    public SkillCode SkillCode { get; set; }
    public int RequiredLevel { get; set; }

    public UnitSkillDefinition(SkillCode skillCode, int requiredLevel)
    {
        SkillCode = skillCode;
        RequiredLevel = requiredLevel;
    }
}
