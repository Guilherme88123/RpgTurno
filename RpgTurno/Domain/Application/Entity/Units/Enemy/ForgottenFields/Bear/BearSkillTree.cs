using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.Bear;

public class BearSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.ClawSwipe, 1),
        new UnitSkillDefinition(SkillCode.SavageMaul, 1),
    ];
}
