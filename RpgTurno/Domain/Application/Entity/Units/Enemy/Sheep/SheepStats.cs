using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Sheep;

public class SheepStats : BaseUnitStats
{
    public SheepStats(int level) : base(level)
    {
        MaxHealthStat = new(134, 12);
        AttackStat = new(24, 2);
        DefenseStat = new(7, 1);
        SpeedStat = new(10, 0.3f);
        MaxManaStat = new(14, 1);
        ManaRegenStat = new(2, 0.1f);
        Accuracy = 91;
        Evasion = 4;
        CriticalChance = 3;
        CriticalDamage = 1.8f;

        Initialize();
    }
}
