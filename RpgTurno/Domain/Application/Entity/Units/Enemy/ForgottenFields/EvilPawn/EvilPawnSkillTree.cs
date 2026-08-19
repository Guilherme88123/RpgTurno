using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.EvilPawn;

public class EvilPawnSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.ImprovisedStrike, 1),
        new UnitSkillDefinition(SkillCode.Repair, 1),
    ];
}
