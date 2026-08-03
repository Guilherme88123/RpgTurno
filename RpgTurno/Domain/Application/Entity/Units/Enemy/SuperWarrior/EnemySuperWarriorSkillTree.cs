using Domain.Enum.Skill;
using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;

namespace Domain.Application.Entity.Units.Enemy.SuperWarrior;

public class EnemySuperWarriorSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Slash, 1),
        new UnitSkillDefinition(SkillCode.HeavySlash, 1),
        new UnitSkillDefinition(SkillCode.GuardStance, 2),
        new UnitSkillDefinition(SkillCode.Cleave, 3),
        new UnitSkillDefinition(SkillCode.Execution, 4),
    ];
}
