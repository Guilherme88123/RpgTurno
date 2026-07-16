using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Cleric;

public class ClericStats : BaseUnitStats
{
    public ClericStats(int level) : base(level)
    {
        MaxHealthStat = new(180, 20);
        AttackStat = new(24, 2.5f);
        DefenseStat = new(18, 2);
        SpeedStat = new(18, 0.8f);
        MaxManaStat = new(40, 5);
        ManaRegenStat = new(6, 0.5f);
        Accuracy = 92;
        Evasion = 8;
        CriticalChance = 5;
        CriticalDamage = 1.5f;

        Initialize();
    }
}
