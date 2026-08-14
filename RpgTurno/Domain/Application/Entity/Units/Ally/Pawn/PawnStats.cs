using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Ally.Pawn;

public class PawnStats : BaseUnitStats
{
    public PawnStats(int level) : base(level)
    {
        MaxHealthStat = new(185, 20);
        AttackStat = new(46, 4);
        DefenseStat = new(17, 2.2f);
        SpeedStat = new(18, 0.9f);
        MaxManaStat = new(24, 4);
        ManaRegenStat = new(5, 0.5f);
        Accuracy = 94;
        Evasion = 6;
        CriticalChance = 17;
        CriticalDamage = 1.9f;

        Initialize();
    }
}
