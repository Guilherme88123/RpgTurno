using Domain.Dto.Map;
using Domain.Enum.Stage;
using Domain.Application.Entity.Units.Ally.Archer;
using Domain.Application.Entity.Units.Ally.Cleric;
using Domain.Application.Entity.Units.Ally.Lancer;
using Domain.Application.Entity.Units.Ally.Warrior;
using Domain.Application.Entity.Units.Base;

namespace Domain.Dto.Session;

public class GameSession
{
    public List<BaseUnitEntity> Allies { get; set; } = new();
    public MapData Map { get; private set; }

    public StageCode CurrentStageCode { get; set; }

    public Action OnStageCleared { get; set; }

    public bool IsInBattle { get; set; }

    public PlayStatistics Statistics { get; set; }

    public void Initialze(MapData map)
    {
        InitializeAllies();
        InitializeMap(map);
    }

    private void InitializeAllies()
    {
        List<BaseUnitEntity> allies =
        [
            new WarriorEntity(),
            new ArcherEntity(),
            new LancerEntity(),
            new ClericEntity(),
        ];

        Allies = allies;
    }

    private void InitializeMap(MapData map)
    {
        Map = map;
    }
}
