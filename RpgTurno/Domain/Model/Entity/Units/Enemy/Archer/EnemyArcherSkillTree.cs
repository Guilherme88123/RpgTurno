using Domain.Enum.Skill.Type;
using Domain.Model.Entity.Units.Base.Skill.Definition;
using Domain.Model.Entity.Units.Base.Skill.SkillTree;

namespace Domain.Model.Entity.Units.Enemy.Archer;

public class EnemyArcherSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillType.Shoot, 1),
        new UnitSkillDefinition(SkillType.ArrowRain, 1),
    ];
}
