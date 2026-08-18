using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Minotaur;

public class MinotaurStats : BaseUnitStats
{
    public MinotaurStats(int level) : base(level)
    {
        MaxHealthStat = new(250, 27);
        AttackStat = new(45, 4.5f);
        DefenseStat = new(28, 2.8f);
        SpeedStat = new(7, 0.3f);

        MaxManaStat = new(14, 1.5f);
        ManaRegenStat = new(2, 0.1f);

        Accuracy = 88;
        Evasion = 2;

        CriticalChance = 7;
        CriticalDamage = 1.8f;

        Initialize();
    }
}
