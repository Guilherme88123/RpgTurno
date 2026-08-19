using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.PigRider;

public class PigRiderStats : BaseUnitStats
{
    public PigRiderStats(int level) : base(level)
    {
        MaxHealthStat = new(190, 20);
        AttackStat = new(36, 3.6f);
        DefenseStat = new(19, 1.8f);
        SpeedStat = new(17, 0.8f);

        MaxManaStat = new(16, 1.5f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 88;
        Evasion = 7;

        CriticalChance = 7;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
