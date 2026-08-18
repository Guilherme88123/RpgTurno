using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Snake;

public class SnakeStats : BaseUnitStats
{
    public SnakeStats(int level) : base(level)
    {
        MaxHealthStat = new(135, 14);
        AttackStat = new(34, 3.4f);
        DefenseStat = new(12, 1.1f);
        SpeedStat = new(19, 0.9f);

        MaxManaStat = new(22, 2.2f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 93;
        Evasion = 16;

        CriticalChance = 9;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
