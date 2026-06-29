using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Lancer;

public class LancerStats : BaseUnitStats
{
    public LancerStats(int level) : base(level)
    {
        MaxHealthStat = new(11, 4);
        AttackStat = new(2, 1);
        DefenseStat = new(3, 1);
        SpeedStat = new(1, 1);

        Initialize();
    }
}
