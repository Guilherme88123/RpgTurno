using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Skill.Base.Unit;

namespace Domain.Application.Entity.Units.Base.Skill.SkillTree;

public abstract class BaseSkillTree
{
    protected abstract IReadOnlyList<UnitSkillDefinition> Definitions { get; }

    public List<UnitSkill> GetAvaliableSkills(BaseUnitEntity ownerUnit, int level)
    {
        return Definitions
            .Where(x => x.RequiredLevel <= level)
            .Select(x => new UnitSkill(ownerUnit, x.SkillCode))
            .ToList();
    }
}
