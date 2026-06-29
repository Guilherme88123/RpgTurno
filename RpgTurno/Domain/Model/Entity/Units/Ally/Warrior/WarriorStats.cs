using Domain.Model.Entity.Units.Base.Stats;

namespace Domain.Model.Entity.Units.Ally.Warrior;

public class WarriorStats : BaseUnitStats
{
    public WarriorStats(int level) : base(level)
    {
        MaxHealthStat = new(10, 3);
        AttackStat = new(3, 2);
        DefenseStat = new(2, 1);
        SpeedStat = new(4, 1);

        Initialize();
    }
}
