namespace Domain.Model.Entity.Units.Base.Stats;

public abstract class BaseUnitStats
{
    public int Level { get; set; }

    private ScalableStat MaxExperienceStat = new ScalableStat(120, 30);
    private ScalableStat ExperienceRewardStat = new ScalableStat(50, 3);

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
    public int ExperienceReward => ExperienceRewardStat.GetFinalValue(Level);

    public bool IsDead => CurrentHealth <= 0;

    public Action OnLevelUp { get; set; }

    protected BaseUnitStats(int level)
    {
        Level = level;
    }

    protected void Initialize()
    {
        CurrentHealth = MaxHealth;
    }

    public int RecieveAttack(int damage)
    {
        var trueDamage = Math.Max(1, damage - Defense);

        CurrentHealth = Math.Max(0, CurrentHealth - trueDamage);

        return trueDamage;
    }

    public void AddExperience(BaseUnitStats targetEliminatedStats)
    {
        CurrentExperience += targetEliminatedStats.ExperienceReward;

        VerifyLevelUp();
    }

    private void VerifyLevelUp()
    {
        while (CurrentExperience > MaxExperience)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        CurrentExperience -= MaxExperience;
        Level++;
        OnLevelUp?.Invoke();
    }

    public void HealHealth(int value)
    {
        var healAmount = Math.Max(0, value);

        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + value);
    }
}
