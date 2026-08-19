using Domain.Application.Entity.Units.Base.Skill.Definition;
using Domain.Application.Entity.Units.Base.Skill.SkillTree;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.BombFish;

public class BombFishSkillTree : BaseSkillTree
{
    protected override IReadOnlyList<UnitSkillDefinition> Definitions =>
    [
        new UnitSkillDefinition(SkillCode.BombSpit, 1),
        new UnitSkillDefinition(SkillCode.DestructiveBomb, 1),
    ];
}
