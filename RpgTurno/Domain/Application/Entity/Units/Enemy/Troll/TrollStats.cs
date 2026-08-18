using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Troll;

public class TrollStats : BaseUnitStats
{
    public TrollStats(int level) : base(level)
    {
        MaxHealthStat = new(330, 33);
        AttackStat = new(52, 5.2f);
        DefenseStat = new(32, 3.2f);
        SpeedStat = new(5, 0.2f);

        MaxManaStat = new(10, 1.0f);
        ManaRegenStat = new(2, 0.1f);

        Accuracy = 84;
        Evasion = 1;

        CriticalChance = 6;
        CriticalDamage = 1.9f;

        Initialize();
    }
}
