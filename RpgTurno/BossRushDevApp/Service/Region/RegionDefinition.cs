using Domain.Enum.Region;
using RpgTurno.Service.Map.EnemyPool;
using RpgTurno.Service.Map.EnemyPool.Definition;
using RpgTurno.Service.Region.Level;

namespace RpgTurno.Service.Map.Region;

public record RegionDefinition
{
    public RegionCode Code { get; set; }
    public EnemyPoolData EnemyPool => EnemyPoolFactory.Create(Code);
    
    public LevelRangeDefinition EnemyLevelRange { get; set; }

    public RegionDefinition(RegionCode code, int minEnemyLevel, int maxEnemyLevel)
    {
        Code = code;
        EnemyLevelRange = new(minEnemyLevel, maxEnemyLevel);
    }
}
