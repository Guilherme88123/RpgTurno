using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Cleric;

public class ClericStats : BaseUnitStats
{
    public ClericStats(int level) : base(level)
    {
        MaxHealthStat = new(8, 3);
        AttackStat = new(4, 2);
        DefenseStat = new(2, 1);
        SpeedStat = new(3, 1);

        Initialize();
    }
}
