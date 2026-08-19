using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.Gnome;

public class GnomeSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.Slash, 1),
        new UnitSkillDefinition(SkillCode.HeavySlash, 1),
    ];
}
