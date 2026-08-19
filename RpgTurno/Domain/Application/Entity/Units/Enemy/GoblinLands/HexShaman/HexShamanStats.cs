using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.HexShaman;

public class HexShamanStats : BaseUnitStats
{
    public HexShamanStats(int level) : base(level)
    {
        MaxHealthStat = new(165, 17);
        AttackStat = new(36, 3.6f);
        DefenseStat = new(14, 1.3f);
        SpeedStat = new(13, 0.6f);

        MaxManaStat = new(35, 3.5f);
        ManaRegenStat = new(3, 0.25f);

        Accuracy = 92;
        Evasion = 7;

        CriticalChance = 6;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
