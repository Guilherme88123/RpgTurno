using Domain.Enum.Skill;
using Domain.Model.Skill.Archer;
using Domain.Model.Skill.Cleric;
using Domain.Model.Skill.Lancer;
using Domain.Model.Skill.Warrior;

namespace Domain.Model.Skill.Base.Factory;

public static class SkillFactory
{
    public static BaseSkill Create(SkillCode skillCode)
    {
        return skillCode switch
        {
            SkillCode.Slash => new SlashSkill(),
            SkillCode.HeavySlash => new HeavySlashSkill(),
            SkillCode.GuardStance => new GuardStanceSkill(),
            SkillCode.Cleave => new CleaveSkill(),
            SkillCode.Execution => new ExecutionSkill(),
            SkillCode.Pike => new PikeSkill(),
            SkillCode.PiercingStrike => new PiercingStrikeSkill(),
            SkillCode.SpearSweep => new SpearSweepSkill(),
            SkillCode.Fortress => new FortressSkill(),
            SkillCode.LastBastion => new LastBastionSkill(),
            SkillCode.Smite => new SmiteSkill(),
            SkillCode.Heal => new HealSkill(),
            SkillCode.Bless => new BlessSkill(),
            SkillCode.Curse => new CurseSkill(),
            SkillCode.DivineLight => new DivineLightSkill(),
            SkillCode.Shoot => new ShootSkill(),
            SkillCode.PowerShoot => new PowerShootSkill(),
            SkillCode.PoisonShoot => new PoisonShootSkill(),
            SkillCode.ArrowRain => new ArrowRainSkill(),
            SkillCode.Snipe  => new SnipeSkill(),

            _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
        };
    }
}
