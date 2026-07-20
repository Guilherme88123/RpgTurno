using Domain.Enum.Stage;
using Domain.Model.Entity.Units.Base;

namespace Domain.Dto.Session;

public class GameSession
{
    public List<BaseUnitEntity> Allies { get; set; } = new();
    public StageCode CurrentStageCode { get; set; }

    public Action OnStageCleared { get; set; }

    public bool IsInBattle { get; set; }

    public PlayStatistics Statistics { get; set; }
}
