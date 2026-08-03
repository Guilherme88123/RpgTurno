using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Lancer;

public class EnemyLancerStats : BaseUnitStats
{
    public EnemyLancerStats(int level) : base(level)
    {
        MaxHealthStat = new(280, 30);
        AttackStat = new(28, 3);
        DefenseStat = new(30, 3);
        SpeedStat = new(12, 0.5f);
        MaxManaStat = new(18, 2);
        ManaRegenStat = new(2, 0.15f);
        Accuracy = 92;
        Evasion = 6;
        CriticalChance = 5;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
