using Domain.Application.Entity.Units.Base;
using Domain.Dto.Map;

namespace Domain.Dto.Session;

public class GameSessionSave
{
    public List<BaseUnitEntity> Allies { get; set; } = new();
    public MapData Map { get; private set; }

    public GameSessionSave(MapData map, List<BaseUnitEntity> allies)
    {
        Map = map;
        Allies = allies;
    }
}
