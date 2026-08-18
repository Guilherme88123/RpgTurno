using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Skull;

public class SkullStats : BaseUnitStats
{
    public SkullStats(int level) : base(level)
    {
        MaxHealthStat = new(165, 17);
        AttackStat = new(36, 3.6f);
        DefenseStat = new(15, 1.4f);
        SpeedStat = new(16, 0.75f);

        MaxManaStat = new(14, 1.5f);
        ManaRegenStat = new(2, 0.1f);

        Accuracy = 93;
        Evasion = 11;

        CriticalChance = 7;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
