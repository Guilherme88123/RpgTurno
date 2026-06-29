using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Enemy.Lancer;

public class EnemyLancerStats : BaseUnitStats
{
    public EnemyLancerStats(int level) : base(level)
    {
        MaxHealthStat = new(200, 30);
        AttackStat = new(16, 2);
        DefenseStat = new(15, 3);
        SpeedStat = new(5, 0);

        Initialize();
    }
}
