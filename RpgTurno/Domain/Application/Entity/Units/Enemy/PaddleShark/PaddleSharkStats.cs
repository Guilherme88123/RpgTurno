using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.PaddleShark;

public class PaddleSharkStats : BaseUnitStats
{
    public PaddleSharkStats(int level) : base(level)
    {
        MaxHealthStat = new(210, 22);
        AttackStat = new(35, 3.5f);
        DefenseStat = new(24, 2.2f);
        SpeedStat = new(14, 0.6f);

        MaxManaStat = new(16, 1.5f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 92;
        Evasion = 7;

        CriticalChance = 6;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
