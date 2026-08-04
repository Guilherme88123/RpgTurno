using Data.Repositories;
using Domain.Interface.Repositories.Stage;
using Domain.Model.Stage;

namespace Service.Repositories.Stage;

public class StageService : EfRepository<StageModel>, IStageService
{
    public StageService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
