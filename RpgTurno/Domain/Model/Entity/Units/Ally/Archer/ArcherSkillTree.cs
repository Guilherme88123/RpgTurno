using Domain.Enum.Skill;
using Domain.Model.Entity.Units.Base.Skill.Definition;
using Domain.Model.Entity.Units.Base.Skill.SkillTree;

namespace Domain.Model.Entity.Units.Ally.Archer;

public class ArcherSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Shoot, 1),
        new UnitSkillDefinition(SkillCode.PowerShoot, 1),
        new UnitSkillDefinition(SkillCode.PoisonShoot, 2),
        new UnitSkillDefinition(SkillCode.ArrowRain, 2),
        new UnitSkillDefinition(SkillCode.Snipe, 3),
    ];
}
