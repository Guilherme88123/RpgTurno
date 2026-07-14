namespace Domain.Model.Entity.Units.Base.Stats;

public abstract class BaseUnitStats
{
    public int Level { get; set; }

    private ScalableStat MaxExperienceStat = new ScalableStat(120, 30);
    private ScalableStat ExperienceRewardStat = new ScalableStat(50, 3);

    public int Accuracy { get; protected set; }
    public int Evasion { get; protected set; }

    public int CriticalChance { get; protected set; }
    public float CriticalDamage { get; protected set; }

    protected ScalableStat MaxHealthStat;
    protected ScalableStat AttackStat;
    protected ScalableStat DefenseStat;
    protected ScalableStat SpeedStat;
    protected ScalableStat MaxManaStat;
    protected ScalableStat ManaRegenStat;

    public int CurrentExperience { get; set; }
    public int CurrentHealth { get; set; }
    public int CurrentMana { get; set; }

    public int MaxExperience => MaxExperienceStat.GetFinalValue(Level);
    public int ExperienceReward => ExperienceRewardStat.GetFinalValue(Level);
    public int MaxHealth => MaxHealthStat.GetFinalValue(Level);
    public int Attack => AttackStat.GetFinalValue(Level);
    public int Defense => DefenseStat.GetFinalValue(Level);
    public int Speed => SpeedStat.GetFinalValue(Level);
    public int MaxMana => MaxManaStat.GetFinalValue(Level);
    public int ManaRegen => ManaRegenStat.GetFinalValue(Level);

    public bool IsDead => CurrentHealth <= 0;

    public Action OnLevelUp { get; set; }

    protected BaseUnitStats(int level)
    {
        Level = level;
    }

    protected void Initialize()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
    }

    #region Attack

    public int RecieveDamage(int damage)
    {
        CurrentHealth = Math.Max(0, CurrentHealth - damage);

        return damage;
    }

    #endregion

    #region Heal

    public int HealHealth(int value)
    {
        var healAmount = Math.Max(0, value);

        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + value);

        return healAmount;
    }

    #endregion

    #region Level Up

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

    #endregion

    #region Mana

    public bool CanSpendMana(int amount)
    {
        return CurrentMana >= amount;
    }

    public bool SpendMana(int amount)
    {
        if (!CanSpendMana(amount))
            return false;

        CurrentMana -= amount;

        return true;
    }

    public void RecoveryMana(int amount)
    {
        CurrentMana = Math.Min(MaxMana, CurrentMana + amount);
    }

    #endregion
}
