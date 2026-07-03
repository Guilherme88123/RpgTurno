using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Archer;

public class ArcherStats : BaseUnitStats
{
    public ArcherStats(int level) : base(level)
    {
        MaxHealthStat = new(90, 12);
        AttackStat = new(3500, 5);
        DefenseStat = new(3, 1);
        SpeedStat = new(14, 2);

        Initialize();
    }
}
