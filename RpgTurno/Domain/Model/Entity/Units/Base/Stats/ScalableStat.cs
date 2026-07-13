namespace Domain.Model.Entity.Units.Base.Stats;

public class ScalableStat
{
    public int BaseValue { get; }
    public float PerLevel { get; }

    public ScalableStat(int baseValue, int perLevel)
    {
        BaseValue = baseValue;
        PerLevel = perLevel;
    }

    public ScalableStat(int baseValue, float perLevel)
    {
        BaseValue = baseValue;
        PerLevel = perLevel;
    }

    public int GetFinalValue(int level)
    {
        return (int)(BaseValue + PerLevel * (level - 1));
    }
}
