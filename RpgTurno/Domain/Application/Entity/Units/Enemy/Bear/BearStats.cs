using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Bear;

public class BearStats : BaseUnitStats
{
    public BearStats(int level) : base(level)
    {
        MaxHealthStat = new(285, 31);
        AttackStat = new(32, 3.2f);
        DefenseStat = new(34, 3.5f);
        SpeedStat = new(10, 0.4f);

        MaxManaStat = new(18, 2);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 90;
        Evasion = 4;

        CriticalChance = 5;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
