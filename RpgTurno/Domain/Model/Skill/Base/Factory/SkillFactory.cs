using Domain.Enum.Skill;

namespace Domain.Model.Skill.Base.Factory;

public static class SkillFactory
{
    public static BaseSkill Create(SkillCode skillCode)
    {
        return skillCode switch
        {
            SkillCode.Slash => new SlashSkill(),
            SkillCode.Pierce => new PierceSkill(),
            SkillCode.Shoot => new ShootSkill(),
            SkillCode.Curse => new CurseSkill(),
            SkillCode.ArrowRain => new ArrowRainSkill(),
            SkillCode.Heal => new HealSkill(),
            SkillCode.Defend => new DefendSkill(),
            SkillCode.Rage => new RageSkill(),

            _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
        };
    }
}
