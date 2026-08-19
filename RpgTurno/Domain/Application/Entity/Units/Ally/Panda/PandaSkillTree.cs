using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Ally.Panda;

public class PandaSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Slash, 1),
        new UnitSkillDefinition(SkillCode.HeavySlash, 1),
    ];
}
