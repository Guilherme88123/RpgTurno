using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Lizard;

public class LizardStats : BaseUnitStats
{
    public LizardStats(int level) : base(level)
    {
        MaxHealthStat = new(145, 15);
        AttackStat = new(28, 2.8f);
        DefenseStat = new(13, 1.2f);
        SpeedStat = new(17, 0.8f);

        MaxManaStat = new(20, 2);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 91;
        Evasion = 14;

        CriticalChance = 7;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
