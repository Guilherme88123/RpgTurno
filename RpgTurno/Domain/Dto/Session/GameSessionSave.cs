using Domain.Application.Entity.Units.Base;
using Domain.Dto.Map;

namespace Domain.Dto.Session;

public class GameSessionSave
{
    public Guid SaveId { get; private set; }
    public List<BaseUnitEntity> Allies { get; set; } = new();
    public MapData Map { get; private set; }

    public GameSessionSave(Guid saveId, MapData map, List<BaseUnitEntity> allies)
    {
        SaveId = saveId;
        Map = map;
        Allies = allies;
    }
}
