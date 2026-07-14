using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Lancer;

public class LancerStats : BaseUnitStats
{
    public LancerStats(int level) : base(level)
    {
        MaxHealthStat = new(200, 30);
        AttackStat = new(16, 2);
        DefenseStat = new(13, 3);
        SpeedStat = new(5, 0);
        MaxManaStat = new(18, 2);
        ManaRegenStat = new(2, 0.2f);
        Accuracy = 90;
        Evasion = 3;
        CriticalChance = 3;
        CriticalDamage = 1.4f;

        Initialize();
    }
}
