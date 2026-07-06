using Domain.Enum.Skill.Type;

namespace Domain.Model.Skill.Base.Factory;

public static class SkillFactory
{
    public static BaseSkill Create(SkillType skillCode)
    {
        return skillCode switch
        {
            SkillType.Slash => new SlashSkill(),
            SkillType.Pierce => new PierceSkill(),
            SkillType.Shoot => new ShootSkill(),
            SkillType.Curse => new CurseSkill(),

            _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
        };
    }
}
