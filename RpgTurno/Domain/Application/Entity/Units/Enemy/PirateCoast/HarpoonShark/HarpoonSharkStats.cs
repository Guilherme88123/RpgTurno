using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.HarpoonShark;

public class HarpoonSharkStats : BaseUnitStats
{
    public HarpoonSharkStats(int level) : base(level)
    {
        MaxHealthStat = new(195, 20);
        AttackStat = new(43, 4.3f);
        DefenseStat = new(20, 1.8f);
        SpeedStat = new(11, 0.5f);

        MaxManaStat = new(18, 2);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 94;
        Evasion = 5;

        CriticalChance = 7;
        CriticalDamage = 1.7f;

        Initialize();
    }
}
