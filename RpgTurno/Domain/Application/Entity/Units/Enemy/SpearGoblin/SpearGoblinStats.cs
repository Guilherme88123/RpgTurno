using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.SpearGoblin;

public class SpearGoblinStats : BaseUnitStats
{
    public SpearGoblinStats(int level) : base(level)
    {
        MaxHealthStat = new(185, 19);
        AttackStat = new(40, 4.0f);
        DefenseStat = new(20, 1.9f);
        SpeedStat = new(13, 0.6f);

        MaxManaStat = new(16, 1.5f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 95;
        Evasion = 6;

        CriticalChance = 7;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
