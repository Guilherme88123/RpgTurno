using Domain.Application.Entity.Units.Base;
using Domain.Dto.Map;
using Domain.Dto.Map.Node;
using Domain.Enum.Stage;

namespace Domain.Dto.Session;

public class GameSession
{
    public GameSessionSave Save { get; private set; }

    public StageCode CurrentStageCode { get; set; }

    public Action OnStageCleared { get; set; }

    public bool IsInBattle { get; set; }

    public PlayStatistics Statistics { get; set; }

    public void InitialzeSave(GameSessionSave save)
    {
        Save = save;
    }
}
