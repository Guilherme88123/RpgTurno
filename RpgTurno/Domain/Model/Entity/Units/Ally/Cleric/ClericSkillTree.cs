using Domain.Enum.Skill;
using Domain.Model.Entity.Units.Base.Skill.Definition;
using Domain.Model.Entity.Units.Base.Skill.SkillTree;

namespace Domain.Model.Entity.Units.Ally.Cleric;

public class ClericSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Smite, 1),
        new UnitSkillDefinition(SkillCode.Heal, 1),
        new UnitSkillDefinition(SkillCode.Bless, 2),
        new UnitSkillDefinition(SkillCode.Curse, 2),
        new UnitSkillDefinition(SkillCode.DivineLight, 3),
    ];
}
