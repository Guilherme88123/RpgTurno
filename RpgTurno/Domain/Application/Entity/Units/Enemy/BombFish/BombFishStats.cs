using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.BombFish;

public class BombFishStats : BaseUnitStats
{
    public BombFishStats(int level) : base(level)
    {
        MaxHealthStat = new(220, 24);
        AttackStat = new(42, 4.2f);
        DefenseStat = new(18, 1.8f);
        SpeedStat = new(8, 0.35f);

        MaxManaStat = new(16, 1.5f);
        ManaRegenStat = new(2, 0.1f);

        Accuracy = 88;
        Evasion = 3;

        CriticalChance = 8;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
