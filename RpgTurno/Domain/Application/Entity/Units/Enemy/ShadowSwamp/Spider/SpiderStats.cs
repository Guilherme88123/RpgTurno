using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Spider;

public class SpiderStats : BaseUnitStats
{
    public SpiderStats(int level) : base(level)
    {
        MaxHealthStat = new(190, 20);
        AttackStat = new(25, 2.5f);
        DefenseStat = new(18, 1.7f);
        SpeedStat = new(11, 0.5f);

        MaxManaStat = new(26, 2.5f);
        ManaRegenStat = new(3, 0.2f);

        Accuracy = 91;
        Evasion = 8;

        CriticalChance = 5;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
