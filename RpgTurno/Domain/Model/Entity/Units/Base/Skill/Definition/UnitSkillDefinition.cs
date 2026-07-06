using Domain.Enum.Skill.Type;

namespace Domain.Model.Entity.Units.Base.Skill.Definition;

public record UnitSkillDefinition
{
    public SkillType SkillCode { get; set; }
    public int RequiredLevel { get; set; }

    public UnitSkillDefinition(SkillType skillCode, int requiredLevel)
    {
        SkillCode = skillCode;
        RequiredLevel = requiredLevel;
    }
}
