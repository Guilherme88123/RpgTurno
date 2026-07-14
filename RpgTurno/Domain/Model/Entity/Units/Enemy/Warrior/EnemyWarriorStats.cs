using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Enemy.Warrior;

public class EnemyWarriorStats : BaseUnitStats
{
    public EnemyWarriorStats(int level) : base(level)
    {
        MaxHealthStat = new(140, 20);
        AttackStat = new(22, 3);
        DefenseStat = new(10, 2);
        SpeedStat = new(8, 1);
        MaxManaStat = new(20, 2);
        ManaRegenStat = new(3, 0.2f);
        Accuracy = 95;
        Evasion = 5;
        CriticalChance = 5;
        CriticalDamage = 1.5f;

        Initialize();
    }
}
