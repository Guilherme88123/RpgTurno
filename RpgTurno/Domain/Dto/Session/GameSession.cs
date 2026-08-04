using Domain.Application.Entity.Units.Base;
using Domain.Dto.Map;
using Domain.Enum.Stage;

namespace Domain.Dto.Session;

public class GameSession
{
    public GameSessionSave Save { get; private set; }

    public StageCode CurrentStageCode { get; set; }

    public Action OnStageCleared { get; set; }

    public bool IsInBattle { get; set; }

    public PlayStatistics Statistics { get; set; }

    public void Initialze(MapData map, List<BaseUnitEntity> allies)
    {
        Save = new(map, allies);
    }

    //private void InitializeAllies()
    //{
    //    List<BaseUnitEntity> allies =
    //    [
    //        new WarriorEntity(),
    //        new ArcherEntity(),
    //        new LancerEntity(),
    //        new ClericEntity(),
    //    ];

    //    Allies = allies;
    //}
}
