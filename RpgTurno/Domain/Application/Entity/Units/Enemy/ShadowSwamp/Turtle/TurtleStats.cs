using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Turtle;

public class TurtleStats : BaseUnitStats
{
    public TurtleStats(int level) : base(level)
    {
        MaxHealthStat = new(300, 30);
        AttackStat = new(22, 2.2f);
        DefenseStat = new(40, 4.0f);
        SpeedStat = new(6, 0.25f);

        MaxManaStat = new(20, 2.0f);
        ManaRegenStat = new(3, 0.2f);

        Accuracy = 88;
        Evasion = 2;

        CriticalChance = 3;
        CriticalDamage = 1.5f;

        Initialize();
    }
}
