using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Gnoll;

public class GnollStats : BaseUnitStats
{
    public GnollStats(int level) : base(level)
    {
        MaxHealthStat = new(170, 18);
        AttackStat = new(38, 3.8f);
        DefenseStat = new(16, 1.5f);
        SpeedStat = new(16, 0.7f);

        MaxManaStat = new(20, 2);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 90;
        Evasion = 8;

        CriticalChance = 10;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
