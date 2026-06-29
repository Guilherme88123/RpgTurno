using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Enemy.Cleric;

public class EnemyClericStats : BaseUnitStats
{
    public EnemyClericStats(int level) : base(level)
    {
        MaxHealthStat = new(120, 15);
        AttackStat = new(12, 2);
        DefenseStat = new(8, 1);
        SpeedStat = new(10, 1);

        Initialize();
    }
}
