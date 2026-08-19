using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Gnome;

public class GnomeStats : BaseUnitStats
{
    public GnomeStats(int level) : base(level)
    {
        MaxHealthStat = new(155, 16);
        AttackStat = new(34, 3.4f);
        DefenseStat = new(14, 1.3f);
        SpeedStat = new(19, 0.9f);

        MaxManaStat = new(16, 1.5f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 94;
        Evasion = 12;

        CriticalChance = 8;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
