using Domain.Enum.Skill;
using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;

namespace Domain.Application.Entity.Units.Enemy.Cleric;

public class EnemyClericSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Smite, 1),
        new UnitSkillDefinition(SkillCode.Heal, 1),
        new UnitSkillDefinition(SkillCode.Bless, 2),
        new UnitSkillDefinition(SkillCode.Curse, 3),
        new UnitSkillDefinition(SkillCode.DivineLight, 4),
    ];
}
