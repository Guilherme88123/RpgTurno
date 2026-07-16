using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Warrior;

public class WarriorStats : BaseUnitStats
{
    public WarriorStats(int level) : base(level)
    {
        MaxHealthStat = new(220, 24);
        AttackStat = new(42, 4);
        DefenseStat = new(20, 2.4f);
        SpeedStat = new(16, 0.7f);
        MaxManaStat = new(20, 3);
        ManaRegenStat = new(3, 0.2f);
        Accuracy = 95;
        Evasion = 5;
        CriticalChance = 10;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
