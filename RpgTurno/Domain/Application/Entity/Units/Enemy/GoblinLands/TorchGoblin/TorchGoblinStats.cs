using Domain.Application.Entity.Units.Base.Stats;

namespace Domain.Application.Entity.Units.Enemy.TorchGoblin;

public class TorchGoblinStats : BaseUnitStats
{
    public TorchGoblinStats(int level) : base(level)
    {
        MaxHealthStat = new(145, 15);
        AttackStat = new(37, 3.7f);
        DefenseStat = new(14, 1.3f);
        SpeedStat = new(10, 0.45f);

        MaxManaStat = new(20, 2.0f);
        ManaRegenStat = new(2, 0.15f);

        Accuracy = 90;
        Evasion = 5;

        CriticalChance = 6;
        CriticalDamage = 1.6f;

        Initialize();
    }
}
