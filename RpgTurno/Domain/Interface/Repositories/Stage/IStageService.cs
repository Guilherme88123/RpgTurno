using Domain.Interface.Repositories.Base;
using Domain.Model.Stage;

namespace Domain.Interface.Repositories.Stage;

public interface IStageService : IRepository<StageModel>
{
    Task<List<StageModel>> GetBySaveAsync(Guid saveId);
}
