using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.Thief;

public class ThiefStats : BaseUnitStats
{
    public ThiefStats(int level) : base(level)
    {
        MaxHealthStat = new(155, 16);
        AttackStat = new(35, 3.5f);
        DefenseStat = new(13, 1.2f);
        SpeedStat = new(18, 0.8f);

        MaxManaStat = new(18, 1.8f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 92;
        Evasion = 18;

        CriticalChance = 18;
        CriticalDamage = 1.8f;

        Initialize();
    }
}
