using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.EvilLancer;

public class EvilLancerSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Pike, 1),
        new UnitSkillDefinition(SkillCode.PiercingStrike, 1),
        new UnitSkillDefinition(SkillCode.Fortress, 2),
        new UnitSkillDefinition(SkillCode.SpearSweep, 3),
        new UnitSkillDefinition(SkillCode.LastBastion, 4),
    ];
}
