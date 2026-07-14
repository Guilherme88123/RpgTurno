using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Archer;

public class ArcherStats : BaseUnitStats
{
    public ArcherStats(int level) : base(level)
    {
        MaxHealthStat = new(90, 12);
        AttackStat = new(35, 5);
        DefenseStat = new(3, 1);
        SpeedStat = new(14, 2);
        MaxManaStat = new(30, 3);
        ManaRegenStat = new(4, 0.3f);
        Accuracy = 100;
        Evasion = 12;
        CriticalChance = 20;
        CriticalDamage = 1.8f;

        Initialize();
    }
}
