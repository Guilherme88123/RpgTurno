namespace Domain.Model.Entity.Units.Base.Stats;

public class ScalableStat
{
    public int BaseValue { get; }
    public int PerLevel { get; }

    public ScalableStat(int baseValue, int perLevel)
    {
        BaseValue = baseValue;
        PerLevel = perLevel;
    }

    public int GetFinalValue(int level)
    {
        return BaseValue + PerLevel * (level - 1);
    }
}
