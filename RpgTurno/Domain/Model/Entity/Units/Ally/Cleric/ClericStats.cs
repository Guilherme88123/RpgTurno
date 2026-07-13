using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Cleric;

public class ClericStats : BaseUnitStats
{
    public ClericStats(int level) : base(level)
    {
        MaxHealthStat = new(120, 15);
        AttackStat = new(12, 2);
        DefenseStat = new(8, 1);
        SpeedStat = new(10, 1);
        MaxManaStat = new(40, 4);
        ManaRegenStat = new(6, 0.4f);

        Initialize();
    }
}
