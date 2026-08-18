using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Panda;

public class PandaStats : BaseUnitStats
{
    public PandaStats(int level) : base(level)
    {
        MaxHealthStat = new(225, 23);
        AttackStat = new(31, 3.1f);
        DefenseStat = new(25, 2.4f);
        SpeedStat = new(15, 0.7f);

        MaxManaStat = new(18, 1.8f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 94;
        Evasion = 9;

        CriticalChance = 9;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
