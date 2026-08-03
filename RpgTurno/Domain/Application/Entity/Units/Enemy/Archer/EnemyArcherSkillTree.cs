using Domain.Enum.Skill;
using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;

namespace Domain.Application.Entity.Units.Enemy.Archer;

public class EnemyArcherSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Shoot, 1),
        new UnitSkillDefinition(SkillCode.PowerShoot, 1),
        new UnitSkillDefinition(SkillCode.PoisonShoot, 2),
        new UnitSkillDefinition(SkillCode.ArrowRain, 3),
        new UnitSkillDefinition(SkillCode.Snipe, 4),
    ];
}
