using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.EvilArcher;

public class EvilArcherStats : BaseUnitStats
{
    public EvilArcherStats(int level) : base(level)
    {
        MaxHealthStat = new(150, 16);
        AttackStat = new(50, 5);
        DefenseStat = new(10, 1);
        SpeedStat = new(24, 1.1f);
        MaxManaStat = new(30, 4);
        ManaRegenStat = new(4, 0.35f);
        Accuracy = 98;
        Evasion = 10;
        CriticalChance = 20;
        CriticalDamage = 1.9f;

        Initialize();
    }
}
