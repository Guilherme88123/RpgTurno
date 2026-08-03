using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.SuperWarrior;

public class EnemySuperWarriorStats : BaseUnitStats
{
    public EnemySuperWarriorStats(int level) : base(level)
    {
        MaxHealthStat = new(220, 24);
        AttackStat = new(42, 4);
        DefenseStat = new(20, 2.4f);
        SpeedStat = new(16, 0.7f);
        MaxManaStat = new(20, 3);
        ManaRegenStat = new(3, 0.2f);
        Accuracy = 94;
        Evasion = 7;
        CriticalChance = 10;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
