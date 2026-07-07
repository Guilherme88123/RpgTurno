using Domain.Enum.Skill;

namespace Domain.Model.Entity.Units.Base.Skill.Definition;

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
