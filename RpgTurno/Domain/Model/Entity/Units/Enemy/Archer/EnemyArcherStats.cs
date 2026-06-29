using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Enemy.Archer;

public class EnemyArcherStats : BaseUnitStats
{
    public EnemyArcherStats(int level) : base(level)
    {
        MaxHealthStat = new(90, 12);
        AttackStat = new(35, 5);
        DefenseStat = new(3, 1);
        SpeedStat = new(14, 2);

        Initialize();
    }
}
