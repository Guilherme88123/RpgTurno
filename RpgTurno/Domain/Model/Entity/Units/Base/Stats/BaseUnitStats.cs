namespace Domain.Model.Entity.Units.Base.Stats;

public abstract class BaseUnitStats
{
    public int Level { get; set; }

    private ScalableStat MaxExperienceStat = new ScalableStat(10, 7);

    protected ScalableStat MaxHealthStat;
    protected ScalableStat AttackStat;
    protected ScalableStat DefenseStat;
    protected ScalableStat SpeedStat;

    public int CurrentExperience { get; set; }
    public int CurrentHealth { get; set; }

    public int MaxExperience => MaxExperienceStat.GetFinalValue(Level);
    public int MaxHealth => MaxHealthStat.GetFinalValue(Level);
    public int Attack => AttackStat.GetFinalValue(Level);
    public int Defense => DefenseStat.GetFinalValue(Level);
    public int Speed => SpeedStat.GetFinalValue(Level);

    protected BaseUnitStats(int level)
    {
        Level = level;
    }

    protected void Initialize()
    {
        CurrentExperience = MaxExperience;
        CurrentHealth = MaxHealth;
    }

    public bool TakeDamage(int value)
    {
        CurrentHealth = Math.Max(0, CurrentHealth - value);

        return CurrentHealth > 0;
    }

    public void HealHealth(int value)
    {
        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + value);
    }
}
