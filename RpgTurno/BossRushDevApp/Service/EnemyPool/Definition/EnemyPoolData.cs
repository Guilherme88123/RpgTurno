using System.Collections.Generic;

namespace RpgTurno.Service.Map.EnemyPool.Definition;

public record EnemyPoolData(IReadOnlyList<EnemyDefinition> Enemies);