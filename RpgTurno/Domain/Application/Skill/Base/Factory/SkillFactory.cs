using Domain.Application.Skill.Archer;
using Domain.Application.Skill.Bear;
using Domain.Application.Skill.BombFish;
using Domain.Application.Skill.Cleric;
using Domain.Application.Skill.Lancer;
using Domain.Application.Skill.Pawn;
using Domain.Application.Skill.Sheep;
using Domain.Application.Skill.Warrior;
using Domain.Enum.Skill;

namespace Domain.Application.Skill.Base.Factory;

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
            SkillCode.Snipe => new SnipeSkill(),
            SkillCode.Bite => new BiteSkill(),
            SkillCode.Pasture => new PastureSkill(),
            SkillCode.ImprovisedStrike => new ImprovisedStrikeSkill(),
            SkillCode.Repair => new RepairSkill(),
            SkillCode.ClawSwipe => new ClawSwipeSkill(),
            SkillCode.SavageMaul => new SavageMaulSkill(),
            SkillCode.BombSpit => new BombSpitSkill(),
            SkillCode.DestructiveBomb => new DestructiveBombSkill(),

            _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
        };
    }
}
