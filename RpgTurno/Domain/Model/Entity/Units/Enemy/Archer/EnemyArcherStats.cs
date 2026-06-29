using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Enemy.Archer;

public class EnemyArcherStats : BaseUnitStats
{
    public EnemyArcherStats(int level) : base(level)
    {
        MaxHealthStat = new(7, 3);
        AttackStat = new(5, 2);
        DefenseStat = new(1, 1);
        SpeedStat = new(5, 2);

        Initialize();
    }
}
